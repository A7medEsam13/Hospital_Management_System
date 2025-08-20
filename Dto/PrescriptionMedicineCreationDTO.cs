namespace Hospital_Management_System.Dto
{
    public class PrescriptionMedicineCreationDTO
    {
        public int PrescriptionID { get; set; }
        public int MedicineID { get; set; }
        public string Dosage { get; set; }
        public string Duration { get; set; }
    }
}
