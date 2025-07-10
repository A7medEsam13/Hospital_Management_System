namespace Hospital_Management_System.Models
{
    public class Diagnosis
    {
        public int Id { set; get; }
        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        public string Details { get; set; }


        // Navigation Properties
        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }
    }
}
