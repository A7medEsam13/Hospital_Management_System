using System.ComponentModel.DataAnnotations;

namespace Hospital_Management_System.Dto
{
    public enum UserRole
    {
        Admin,
        Doctor,
        Nurse,
        Receptionist,
        Patient
    }
    public class RegisterDto
    {
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.EmailAddress)] 
        public string Email { get; set; }
        public UserRole UserRole { get; set; }
    }
}
