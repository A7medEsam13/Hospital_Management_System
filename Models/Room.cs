

namespace Hospital_Management_System.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public decimal Cost { get; set; }
        public string DepartmentName { set; get; }
        public int Capacity { set; get; }
        public int NumberOfPatients { set; get; }

        public ICollection<Patient> Patients { get; set; } // Navigation property to Patient
        public ICollection<Bill> Bills { get; set; }
    }
}
