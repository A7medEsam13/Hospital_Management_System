namespace Hospital_Management_System.Services
{
    public interface IEmergencyContactServices
    {
        public Task AddAsync(EmergencyContactCreationDto emergencyContact); //
        public Task DeleteAsync(int id); //
        public IEnumerable<EmergencyContactDisplayDto> GetAllPatientEmergencyContactsAsync(int patientId); //
        public Task<EmergencyContactDisplayDto> GetById(int id);
        public Task DeleteAllPatientEmergencyContacts(int patientId); //
    }
}
