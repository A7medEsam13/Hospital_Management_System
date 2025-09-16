using Microsoft.AspNetCore.Identity;
namespace Hospital_Management_System.Models
{
    public class ApplicationUser : IdentityUser
    {

        public string StuffSSN { set; get; }

        public Stuff Stuff { set; get; }

       
    }
}
