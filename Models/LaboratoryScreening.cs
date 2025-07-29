

namespace Hospital_Management_System.Models
{
    public class LaboratoryScreening
    {
        public int Id { get; set; }
        [ForeignKey("Patient")]
        public int PatientId { get; set; } // Foreign key to Patient
        [ForeignKey("Technician")]
        public string TechnicianSSN { get; set; } // Foreign key to Staff (Technician)
        [ForeignKey("Doctor")]
        public string DoctorId { get; set; } // Foreign key to Doctor
        [ForeignKey("Bill")]
        public int BillId { get; set; }
        public decimal TestCost { get; set; }
        public DateOnly Date { get; set; }

        // Navigation properties
        public Patient Patient { get; set; }
        public Stuff Technician { get; set; } // Assuming Staff is a base class for all staff members
        public Doctor Doctor { get; set; } // Assuming Doctor is a subclass of Staff
        public Bill Bill { get; set; }
    }
}
