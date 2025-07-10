using Hospital_Management_System.Models;

namespace Hospital_Management_System.Services
{
    public interface IPatientServices
    {
        public Task AddPatient(Patient patient);
        public Task RemovePatient(int patientId);
        public Patient GetPatientById(int patientId);
        public Task<Patient> GetPatientByName(string name);
        public Task<ICollection<Patient>> GetAllPatients();
        public void Updatepatient(Patient patient);
        public Task SaveAsync();
    }
}
