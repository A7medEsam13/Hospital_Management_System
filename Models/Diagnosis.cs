namespace Hospital_Management_System.Models
{
    public class Diagnosis
    {
        public int Id { set; get; }
        public string Name { get; set; }
        public string Details { get; set; }
        public string DoctorSSN { get; set; }


        // Navigation Properties
        public ICollection<DiagnosisPatient> DiagnosisPatient { get; set; }
        public Doctor Doctor { get; set; }
    }
}
