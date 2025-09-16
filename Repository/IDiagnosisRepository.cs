namespace Hospital_Management_System.Repository
{
    public interface IDiagnosisRepository
    {
        public Task AddAsync(Diagnosis diagnosis);
        public Task UpdateAsync(Diagnosis diagnosis);
        public Task DeleteAsync(Diagnosis diagnosis);
        public List<Diagnosis> GetAll();
        public Task<Diagnosis> GetByIdAsync(int id);
        public List<Diagnosis> GetAllPatientDiagnosis(int patientId);
        public List<Diagnosis> GetAllDoctorDiagnosis(string doctorSSN);
        public Task SaveAsync();
    }
}
