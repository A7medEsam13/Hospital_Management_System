namespace Hospital_Management_System.Repository
{
    public interface ILaboratoryScreeningRepository
    {
        public Task CreateScreening(LaboratoryScreening screening);
        public Task UpdateScreening(LaboratoryScreening screening);
        public Task DeleteScreening(int id);
        public Task<LaboratoryScreening> GetScreeningByPatientIDAndDoctorSSN(int patientID, string doctorSSN);
        public Task<List<LaboratoryScreening>> GetAllPatientScreenings(int patientID);
        public Task<List<LaboratoryScreening>> GetAllDoctorScreenings(string doctorSSN);
        public Task<List<LaboratoryScreening>> GetAllTechnicanScreenings(string technicanSSN);
        public Task<LaboratoryScreening> GetByID(int id);
    }
}
