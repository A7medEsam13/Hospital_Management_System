namespace Hospital_Management_System.Repository
{
    public interface IPrescriptionMedicineRepository
    {
        public Task CreatePrescriptionMedicine(PrescriptionMedicine prescriptionMedicine);
        public Task<PrescriptionMedicine> GetPrescriptionMedicine(int prescriptionID, int medicineID);
        public Task UpdatePrescriptionMedicine(PrescriptionMedicine prescriptionMedicine);
        public Task Delete(int prescriptionID, int medicineID);
        public List<PrescriptionMedicine> GetMedicinesOfPrescription(int prescriptionID);
        public Task SaveAsync();
    }
}
