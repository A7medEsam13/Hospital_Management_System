namespace Hospital_Management_System.Models
{
    public class Doctor : Stuff
    {
        public string Qualification { get; set; }
        public string Specialization { get; set; }

        // Navigation properties
        public ICollection<Patient> Patients { get; set; }
        public ICollection<Prescription> Prescriptions { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<LaboratoryScreening> LaboratoryScreenings { get; set; }
    }
}
