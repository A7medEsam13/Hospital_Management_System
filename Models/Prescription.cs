

namespace Hospital_Management_System.Models
{
    public class Prescription
    {
        public int Id { get; set; }
        [ForeignKey("Patient")]
        public int PatientId { get; set; } // Foreign key to Patient
        public DateOnly Date { get; set; }
        public int Dosage { get; set; }
        [ForeignKey("Doctor")]
        public string DoctorId { get; set; } // Foreign key to Doctor


        // Navigation properties
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
        public ICollection<Medicine> Medicines { get; set; }
    }
}