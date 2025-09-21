namespace Hospital_Management_System.Services
{
    public interface IDiagnosisServices
    {
        public Task AddAsync(DiagnosisCreationDto diagnosis); //
        public Task UpdateAsync(int id, DiagnosisUpdateDto diagnosis); //
        public Task DeleteAsync(int id); //
        public Task<IEnumerable<DiagnosisDisplayDto>> GetAll(); //
        public Task<DiagnosisDisplayDto> GetByIdAsync(int id);
        public Task<IEnumerable<DiagnosisDisplayDto>> GetAllPatientDiagnosis(int patientId);
        public IEnumerable<Task<DiagnosisDisplayDto>> GetAllDoctorDiagnosis(string doctorSSN);
    }
}
