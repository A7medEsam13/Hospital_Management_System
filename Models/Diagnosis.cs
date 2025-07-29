namespace Hospital_Management_System.Models
{
    public class Diagnosis
    {
        public int Id { set; get; }
        public string Name { get; set; }
        public string Details { get; set; }


        // Navigation Properties
        public ICollection<Patient> Patients { get; set; }
    }
}
