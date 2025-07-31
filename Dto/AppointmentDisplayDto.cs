namespace Hospital_Management_System.Dto
{
    public class AppointmentDisplayDto
    {
        public int Id { get; set; }
        public DateTime ScheduledOn { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public int PatientId { get; set; } // Foreign key to Patient
        public string DoctorId { get; set; }
        public string PatientName { get; set; }
        public string DoctorName { get; set; }
    }
}
