using System.ComponentModel.DataAnnotations;

namespace Hospital_Management_System.Dto
{
    public class PayrollCreateDto
    {
        public decimal Salary { get; set; }
        [MaxLength(16)]
        [MinLength(16)]
        public string StaffSSN { get; set; }
        public string IBAN { get; set; }
    }
}
