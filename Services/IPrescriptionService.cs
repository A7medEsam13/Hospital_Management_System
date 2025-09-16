namespace Hospital_Management_System.Services
{
    public interface IPrescriptionService
    {
        public Task CreateNewPrescription(PrescriptionCreationDto dto);
        public IEnumerable<PrescriptionDisplayDto> GetAllPatientPrescriptions(int patientID);
        public Task DeletePrescriptionAsync(int prescriptionID);
        public IEnumerable<PrescriptionDisplayDto> GetAllDoctorPrescriptions(string doctorSSN);
        public IEnumerable<PrescriptionMedicineDisplayDTO> GetAllPrescriptionMedicines(int prescriptionID); 
        public Task UpdatePrescription(PrescriptionDisplayDto dto);
        public Task AddNewMedicine(PrescriptionMedicineCreationDTO dto);
        public Task RemoveMedicine(int medicineID,int prescriptionID);
    }
}
