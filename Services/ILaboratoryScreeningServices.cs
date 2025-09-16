namespace Hospital_Management_System.Services
{
    public interface ILaboratoryScreeningServices
    {
        public Task CreateLaboratoryScreening(LaboratoryScreeningCreationDto dto);  //
        public Task Delete(int id);
        public Task Update(LaboratoryScreeningUpdateDto dto);       //
        public Task<List<LaboratoryScreeningDisplayDto>> GetAllPatientScreenings(int patientID);       //
        public Task<List<LaboratoryScreeningDisplayDto>> GetAllDoctorScreenings(string doctorSSN);      //
        public Task<List<LaboratoryScreeningDisplayDto>> GetAllTechnicanScreenings(string technicanSSN);        //
        public Task<LaboratoryScreeningDisplayDto> GetScreeningByPatientIDAndDooctorSSN(int patientID, string doctorSSN); //
        public Task<LaboratoryScreeningDisplayDto> GetByID(int id);         //

    }
}
