namespace Hospital_Management_System.Dto
{
    public class PrescriptionMedicineDisplayDTO
    {
        public string Dosage { get; set; }
        public string Duration { set; get; }
        public int PrescriptionId { get; set; }
        public int MedicineId { get; set; }
        public string MedicineName { get; set; }
    }
}
