using System.ComponentModel.DataAnnotations;

namespace Hospital_Management_System.Dto
{
    public class RegisterDto
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [DataType(DataType.EmailAddress)] 
        public string Email { get; set; }
        public string StuffSSN { get; set; }
    }
}
