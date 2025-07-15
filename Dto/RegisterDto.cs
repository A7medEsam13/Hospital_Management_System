using System.ComponentModel.DataAnnotations;

namespace Hospital_Management_System.Dto
{
    public class RegisterDto
    {
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.EmailAddress)] 
        public string Email { get; set; }
    }
}
