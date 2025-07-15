using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleServices _roleServices;
        private readonly ILogger<RolesController> _logger;

        public RolesController(IRoleServices roleServices, 
            ILogger<RolesController> logger)
        {
            _roleServices = roleServices;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(RoleDto roleDto)
        {
            var isAdded = await _roleServices.AddRoleAsync(roleDto);
            if (isAdded)
            {
                _logger.LogInformation("Role with name {RoleName} added successfully.", roleDto.Name);
                return Created();
            }
            else
            {
                _logger.LogError("Role with name {RoleName} already exists.", roleDto.Name);
                return BadRequest("Role already exists.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _roleServices.GetAllRolesAsync();
            if (roles == null || !roles.Any())
            {
                _logger.LogInformation("No roles found.");
                return NotFound("No roles found.");
            }
            _logger.LogInformation("Retrieved {RoleCount} roles.", roles.Count());
            return Ok(roles);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetRoleByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                _logger.LogError("Role name cannot be null or empty.");
                return BadRequest("Role name cannot be null or empty.");
            }
            var role = await _roleServices.GetRoleByNameAsync(name);
            if (role == null)
            {
                _logger.LogInformation("Role with name {RoleName} not found.", name);
                return NotFound($"Role with name {name} not found.");
            }
            _logger.LogInformation("Retrieved role with name {RoleName}.", name);
            return Ok(role);
        }
    }
}
