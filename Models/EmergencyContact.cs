namespace Hospital_Management_System.Models
{
    public class EmergencyContact
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Relation { get; set; }
        [ForeignKey("Patient")]
        public int PatientId { get; set; } // Foreign key to Patient

        // Navigation property
        public Patient Patient { get; set; }
        }
}
