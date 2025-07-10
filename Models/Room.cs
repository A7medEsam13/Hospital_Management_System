

namespace Hospital_Management_System.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Type { get; set; }
        [ForeignKey("Patient")]
        public int PatientId { get; set; } // Foreign key to Patient
        public decimal Cost { get; set; }
        [ForeignKey("Bill")]
        public int BillId { get; set; }

        public Patient Patient { get; set; } // Navigation property to Patient
        public Bill Bill { get; set; }
    }
}
