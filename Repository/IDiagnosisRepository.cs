namespace Hospital_Management_System.Repository
{
    public interface IDiagnosisRepository
    {
        public Task AddAsync(Diagnosis diagnosis);
        public Task UpdateAsync(Diagnosis diagnosis);
        public Task DeleteAsync(Diagnosis diagnosis);
        public IQueryable<Diagnosis> GetAll();
        public Task<Diagnosis> GetByIdAsync(int id);
        public IQueryable<Diagnosis> GetAllPatientDiagnosis(int patientId);
        public IQueryable<Diagnosis> GetAllDoctorDiagnosis(string doctorSSN);
        public Task SaveAsync();
    }
}
