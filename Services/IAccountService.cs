using Microsoft.AspNetCore.Identity;

namespace Hospital_Management_System.Services
{
    public interface IAccountService
    {
        public Task<string> GetUserID(string userName);
        public Task<IdentityResult> DeleteAccount(string userName);
    }
}
