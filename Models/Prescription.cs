

namespace Hospital_Management_System.Models
{
    public class Prescription
    {
        public int Id { get; set; }
        [ForeignKey("Patient")]
        public int PatientId { get; set; } // Foreign key to Patient
        public DateOnly Date { get; set; }
        [ForeignKey("Doctor")]
        public string DoctorId { get; set; } // Foreign key to Doctor
        public bool IsPaid { get; set; } = false;


        public int? BillID { get; set; }
        public Bill? Bill { get; set; }


        // Navigation properties
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
        public ICollection<PrescriptionMedicine> PrescriptionMedicines { get; set; }
        public ICollection<LaboratoryScreeningPrescription> LaboratoryScreeningPrescriptions { get; set; }
    }
}