namespace Hospital_Management_System.Dto
{
    public class LaboratoryScreeningCreationDto
    {
        public int PatientId { get; set; } 
        public string DoctorId { get; set; } 
        public int BillId { get; set; }
        public decimal TestCost { get; set; }
        public DateOnly Date { get; set; }
        public string Name { get; set; }
        public string Report { set; get; }
    }
}
