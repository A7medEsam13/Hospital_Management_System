

namespace Hospital_Management_System.Models
{
    public class MedicalHistory
    {
        public int Id { get; set; }
        [ForeignKey("Patient")]
        public int PatientId { get; set; } // Foreign key to Patient
        public string Allergies { get; set; }
        public string PreCondotion { get; set; }

        // Navigation property
        public Patient Patient { get; set; }
    }
}
