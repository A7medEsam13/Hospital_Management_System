namespace Hospital_Management_System.Models
{
    public class PrescriptionMedicine
    {
        public int PrescriptionId { get; set; }
        public Prescription Prescription { get; set; }

        public int MedicineId { get; set; }
        public Medicine Medicine { get; set; }

        public string Dosage { get; set; }
        public string Duration { set; get; }
    }
}
