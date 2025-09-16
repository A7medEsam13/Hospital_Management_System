namespace Hospital_Management_System.Dto
{
    public class LaboratoryScreeningDisplayDto
    {
        public int ID { get; set; }
        public int PatientID { get; set; }
        public string PatientName { get; set; }
        public string TechnicanName { get; set; }
        public string DoctorName { get; set; }
        public decimal TestCost { get; set; }
        public DateOnly Date { get; set; }
        public string TestName { get; set; }
        public string Report { get; set; }
    }
}
