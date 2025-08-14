namespace Hospital_Management_System.Repository
{
    public interface IEmergencyContactRepository
    {
        public IEnumerable<EmergencyContactDisplayDto> GetAllPatientEmergencyContacts(int patientId);
        public Task<EmergencyContactDisplayDto> GetByIdAsync(int id);
        public Task AddAsync(EmergencyContact emergencyContact);
        public void Delete(EmergencyContact emergencyContact);
        public void DeleteAllPatientEmergencyContacts(IEnumerable<EmergencyContact> emergencyContacts);
        public Task SaveAsync();
    }
}
