using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_Management_System.Models
{
    public class Insurance
    {
        [Key]
        public int PolicyNumber { get; set; }
        [ForeignKey("Patient")]
        public int PatientId { get; set; } // Foreign key to Patient
        public int InsuranceCode { get; set; }
        public DateOnly EndDate { get; set; }
        public string Provider { get; set; }
        public string Plan { get; set; }
        public decimal CoPay { get; set; }
        public string Coverage { get; set; }
        public bool Optical { get; set; }
        public bool Dental { get; set; }

        // Navigation property
        public Patient Patient { get; set; }
    }
}
