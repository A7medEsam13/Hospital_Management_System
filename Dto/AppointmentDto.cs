namespace Hospital_Management_System.Dto
{
    public class AppointmentDto
    {
        public DateTime ScheduledOn { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public int PatientId { get; set; } 
        public int DoctorId { get; set; } 
    }
}
