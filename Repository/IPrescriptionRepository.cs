namespace Hospital_Management_System.Repository
{
    public interface IPrescriptionRepository
    {
        public Task<List<Prescription>> GetUnPaidPrescriptions(int patientID);
        public Task CreateNewPrescriptionAsync(Prescription prescription);
        public Task DeletePrescriptionAsync(int prescriptionID);
        public List<Prescription> GetAllPatientPrescriptions(int patientID);
        public List<Prescription> GetAllDoctorPrescriptions(string doctorSSN);
        public Task Pay(Prescription prescription); 
    }
}
