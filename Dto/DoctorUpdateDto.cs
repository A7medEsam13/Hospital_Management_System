using System.ComponentModel.DataAnnotations;

namespace Hospital_Management_System.Dto
{
    public class DoctorUpdateDto
    {
        [Required(AllowEmptyStrings =false,ErrorMessage ="SSN is required")]
        public string SSN { get; set; }
        [Required(AllowEmptyStrings =false,ErrorMessage ="Address is required")]
        public string Address { get; set; }
        [Required(AllowEmptyStrings =false,ErrorMessage ="Department Name is required")]
        public string DepartmentName { get; set; }
        [Required(AllowEmptyStrings =false,ErrorMessage ="Qualification is required")]
        public string Qualification { get; set; }
        [Required(AllowEmptyStrings =false,ErrorMessage ="Specialization is required")]
        public string Specialization { get; set; }
    }
}
