using Hospital_Management_System.Models;

namespace Hospital_Management_System.Services
{
    public interface IPatientServices
    {
        public Task AddPatient(PatientCreationDto patient);
        public Task RemovePatient(int patientId);
        public Task<Patient> GetPatientById(int patientId);
        public Task<Patient> GetPatientByName(string name);
        public Task<IEnumerable<Patient>> GetAllPatients();
        public Task Updatepatient(PatientUpdateDto patient);
    }
}
