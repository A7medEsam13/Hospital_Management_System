namespace Hospital_Management_System.Models
{
    public class Nurse
    {
        public int Id { get; set; }
        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        [ForeignKey("Staff")]
        public string StaffSSN { get; set; }
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }

        // Navigation properties
        public Patient Patient { get; set; }
        public Staff Staff { get; set; } // Assuming Staff is a base class for all staff members
        public Department Department { get; set; } // Assuming Department is a class representing hospital departments
    }
}
