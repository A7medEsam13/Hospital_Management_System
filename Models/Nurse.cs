namespace Hospital_Management_System.Models
{
    public class Nurse
    {
        public int Id { get; set; }
        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        [ForeignKey("Staff")]
        public string StaffSSN { get; set; }
        public string DepartmentName { get; set; }

        // Navigation properties
        public Patient Patient { get; set; }
        public Stuff Staff { get; set; } // Assuming Staff is a base class for all staff members
    }
}
