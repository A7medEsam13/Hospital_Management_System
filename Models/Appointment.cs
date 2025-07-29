namespace Hospital_Management_System.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime ScheduledOn { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        [ForeignKey("Patient")]
        public int PatientId { get; set; } // Foreign key to Patient
        [ForeignKey("Doctor")]
        public string DoctorId { get; set; } // Foreign key to Doctor

        // Navigation properties
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; } 
    }
}