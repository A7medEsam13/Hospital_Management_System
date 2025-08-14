namespace Hospital_Management_System.Models
{
    public class DiagnosisPatient
    {
        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        public int DiagnosisId { get; set; }
        public Diagnosis Diagnosis { get; set; }
    }
}
