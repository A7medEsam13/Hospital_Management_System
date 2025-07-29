namespace Hospital_Management_System.Repository
{
    public interface IPatientRepository
    {
        public Task AddPatient(Patient patient);
        public Task RemovePatient(int patientId);
        public Task<Patient> GetPatientById(int patientId);
        public IEnumerable<Patient> GetPatientsByName(string name);
        public Task<ICollection<Patient>> GetAllPatients();
        public void Updatepatient(Patient patient);
        public Task SaveAsync();
    }
}
