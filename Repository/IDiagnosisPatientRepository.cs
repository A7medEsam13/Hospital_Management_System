namespace Hospital_Management_System.Repository
{
    public interface IDiagnosisPatientRepository
    {
        public Task AddAsync(DiagnosisPatient diagnosisPatient);
        public DiagnosisPatient GetByDiagnosisId(int diagnosisId);
        public Task<int> GetPatientID(int diagnosisID);
    }
}
