using Hospital_Management_System.Repository;
using Hospital_Management_System.UnitOfWork;
using System.Security.Claims;

namespace Hospital_Management_System.Extensions
{
    public  static class GetData
    {
        public static async Task<Stuff> GetCurrentUserDataAsync(IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork)
        {
            var userID = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var stuff = await unitOfWork.Stuffs.GetStuffByUserID(userID);
            return stuff;
        }
    }
}
