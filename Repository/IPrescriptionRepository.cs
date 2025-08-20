namespace Hospital_Management_System.Repository
{
    public interface IPrescriptionRepository
    {
        public Task CreateNewPrescriptionAsync(Prescription prescription);
        public Task DeletePrescriptionAsync(int prescriptionID);
        public IQueryable<Prescription> GetAllPatientPrescriptions(int patientID);
        public IQueryable<Prescription> GetAllDoctorPrescriptions(string doctorSSN);
        public Task SaveAsync();
    }
}
