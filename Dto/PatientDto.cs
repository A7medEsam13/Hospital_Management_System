namespace Hospital_Management_System.Dto
{
    public class PatientDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string BloodType { get; set; }
        public string Email { get; set; }
        public char Gender { get; set; }
        public string Condition { get; set; }
        public DateOnly AdmissionDate { get; set; }
        public DateOnly DisChargeDate { get; set; }
    }
}
