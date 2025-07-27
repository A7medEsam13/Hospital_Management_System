using System.ComponentModel.DataAnnotations;

namespace Hospital_Management_System.Models
{
    public class Stuff
    {
        [Key]
        public string SSN { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly JoinDate { get; set; }
        public DateOnly SeparationDate { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string DepartmentName { get; set; }
        public bool IsTerminated { get; set; } = false;

        [ForeignKey("User")]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
