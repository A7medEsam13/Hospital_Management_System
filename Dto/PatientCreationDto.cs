using System.ComponentModel.DataAnnotations;

namespace Hospital_Management_System.Dto
{
    public class PatientCreationDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [StringLength(16)]
        public string SSN { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        public string BloodType { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public char Gender { get; set; }
        public string Condition { get; set; }
        public DateOnly AdmissionDate { get; set; }
    }
}
