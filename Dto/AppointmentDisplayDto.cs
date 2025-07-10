namespace Hospital_Management_System.Dto
{
    public class AppointmentDisplayDto
    {
        public int Id { get; set; }
        public DateTime ScheduledOn { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        [ForeignKey("Patient")]
        public int PatientId { get; set; } // Foreign key to Patient
        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }
    }
}
