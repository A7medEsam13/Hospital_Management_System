namespace Hospital_Management_System.Dto
{
    public class PrescriptionDisplayDto
    {
        public int PatientId { set; get; }
        public string DoctorSSN { get; set; }
        public List<PrescriptionMedicineDisplayDTO> Medicines { set; get; }
    }
}
