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

        [ForeignKey("User")]
        public string UserId { get; set; }


        // Navigation property to the User table
        public ApplicationUser User { get; set; }
    }
}
