
using Hospital_Management_System.Repository;
using Hospital_Management_System.UnitOfWork;
using Microsoft.AspNetCore.Identity;

namespace Hospital_Management_System.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public AccountService(UserManager<ApplicationUser> userManager,
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<string> GetUserID(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var id = await _userManager.GetUserIdAsync(user);
            return id;
        }

        public async Task<IdentityResult> DeleteAccount(string userName)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(userName);
            if (user is not null)
            {
                var stuffEntity = await _unitOfWork.Stuffs.GetStuffByUserID(user.Id);
                stuffEntity.UserId = null;
                _unitOfWork.Stuffs.Update(stuffEntity);
                await _unitOfWork.Complete();

                var result = await _userManager.DeleteAsync(user);
                return result;
            }
            else
                throw new Exception("User not found");
        }
    }
}
