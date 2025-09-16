namespace Hospital_Management_System.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string BloodType { get; set; }
        public string Email { get; set; }
        public char Gender { get; set; }
        public string SSN { get; set; }
        public string Condition { get; set; }
        public DateOnly AdmissionDate { get; set; }
        public DateOnly DisChargeDate { get; set; }
        [ForeignKey("Room")]
        public int? RoomId { get; set; } 

        // Navigation Preperties
        public ICollection<Prescription> Prescriptions { get; set; }
        public ICollection<Bill> Bills { get; set; }    
        public ICollection<DiagnosisPatient> DiagnosisPatient { get; set; }   
        public ICollection<Doctor> Doctors { get; set; }
        public ICollection<EmergencyContact> EmergencyContacts { get; set; }
        public Room Room {get; set; } 
    }
}
