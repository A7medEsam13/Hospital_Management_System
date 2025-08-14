using System.ComponentModel.DataAnnotations;

namespace Hospital_Management_System.Dto
{
    public class DoctorCreateDto
    {
        // staff properties
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly JoinDate { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Address { get; set; }
        public string DepartmentName { get; set; }
        public string SSN { get; set; }
        public decimal Salary { get; set; }

        // doctor properties
        public string Qualification { get; set; }
        public string Specialization { get; set; }

        public string UserName { get; set; }
    }
}
