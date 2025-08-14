namespace Hospital_Management_System.Dto
{
    public class DiagnosisDisplayDto
    {
        public int Id { set; get; }
        public string Name { get; set; }
        public string Details { get; set; }
        public string DoctorSSN { get; set; }
        public string DoctorName { get; set; }
        public int patientId { get; set; }
        public string PatientName { get; set; }
    }
}
