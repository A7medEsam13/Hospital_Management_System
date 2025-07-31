using System.ComponentModel.DataAnnotations;

namespace Hospital_Management_System.Dto
{
    public class AppointmentCreationDto
    {
        public DateTime ScheduledOn { get; set; }
        [DataType(DataType.Date)]
        public DateOnly Date { get; set; }
        [DataType(DataType.Time)]
        public TimeOnly Time { get; set; }
        public int PatientId { get; set; } 
        public string DoctorId { get; set; } 
    }
}
