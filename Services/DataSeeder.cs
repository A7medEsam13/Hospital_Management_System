using Hospital_Management_System.UnitOfWork;
using Microsoft.AspNetCore.Identity;

namespace Hospital_Management_System.Services
{
    public class DataSeeder
    {
        public static async Task SeedAdmin(IServiceProvider service)
        {
            var userManager = service.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();
           



            string adminEmail = "aesam4168@gmail.com";
            string adminPass = "Admin@123";

            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if(adminUser is null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    StuffSSN = "30508131401718"
                };

                var result = await userManager.CreateAsync(adminUser, adminPass);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}
