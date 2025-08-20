
using Microsoft.AspNetCore.Identity;

namespace Hospital_Management_System.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> GetUserID(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var id = await _userManager.GetUserIdAsync(user);
            return id;
        }
    }
}
