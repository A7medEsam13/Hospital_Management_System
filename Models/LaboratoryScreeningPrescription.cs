namespace Hospital_Management_System.Models
{
    public class LaboratoryScreeningPrescription
    {
        public int PrescriptionId { get; set; }
        public Prescription Prescription { get; set; }
        public int LaboratoryScreeningId { get; set; }
        public LaboratoryScreening LaboratoryScreening { get; set; }
    }
}
