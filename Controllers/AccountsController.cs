using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Hospital_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;
        private readonly ILogger<AccountsController> _logger;

        public AccountsController(UserManager<ApplicationUser> userManager,
            IConfiguration config,
            ILogger<AccountsController> logger)
        {
            _userManager = userManager;
            _config = config;
            _logger = logger;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (ModelState.IsValid)
            {

                ApplicationUser user = new()
                {
                    Email = dto.Email,
                    UserName = dto.UserName
                };

                IdentityResult result = await _userManager.CreateAsync(user, dto.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, dto.UserRole.ToString());
                    _logger.LogInformation("User {UserName} registered successfully", dto.UserName);
                    return Ok("Created");
                }

                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.Code, item.Description);
                }
            }
            _logger.LogError("Registration failed for user {UserName} with errors: {Errors}", dto.UserName, ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
            return BadRequest(ModelState);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            ApplicationUser userFromDB = await _userManager.FindByNameAsync(dto.UserName);
            if (userFromDB != null)
            {
                bool found = await _userManager.CheckPasswordAsync(userFromDB,dto.Password);
                if (found)
                {
                    // generate token
                    List<Claim> userClaims = new();

                    userClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                    userClaims.Add(new Claim(ClaimTypes.NameIdentifier, userFromDB.Id));
                    userClaims.Add(new Claim(ClaimTypes.Name, userFromDB.UserName));

                    var userRoles = await _userManager.GetRolesAsync(userFromDB);

                    foreach(var role in userRoles)
                    {
                        userClaims.Add(new Claim(ClaimTypes.Role, role));   
                    }

                    var signInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:securityKey"]));

                    SigningCredentials signingCredentials = new(signInKey, SecurityAlgorithms.HmacSha256);

                    // create token
                    JwtSecurityToken token = new(
                        audience: _config["JWT:audience"],
                        issuer: _config["JWT:issuer"],
                        expires: DateTime.Now.AddMinutes(30),
                        claims: userClaims,
                        signingCredentials: signingCredentials
                        );

                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = DateTime.Now.AddMinutes(30)
                    });
                }
                ModelState.AddModelError("UserName", "Username or password Invalid");

            }
            return BadRequest(ModelState);

        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(string userName)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(userName);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User {UserName} deleted successfully", userName);
                    return Ok("Deleted");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.Code, item.Description);
                }
            }
            else
            {
                ModelState.AddModelError("UserName", "User not found");
            }
            _logger.LogError("Deletion failed for user {UserName} with errors: {Errors}", userName, ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
            return BadRequest(ModelState);
        }
    }
}
