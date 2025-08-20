
using Microsoft.EntityFrameworkCore;

namespace Hospital_Management_System.Repository
{
    public class PrescriptionMedicineRepository : IPrescriptionMedicineRepository
    {
        private readonly ApplicationDbContext _context;

        public PrescriptionMedicineRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreatePrescriptionMedicine(PrescriptionMedicine prescriptionMedicine)
        {
            await _context.PrescriptionMedicines.AddAsync(prescriptionMedicine);
        }

        public async Task Delete(int prescriptionID, int medicineID)
        {
            await _context.PrescriptionMedicines
                .Where(pm => pm.PrescriptionId == prescriptionID && pm.MedicineId == medicineID)
                .ExecuteDeleteAsync();
        }

        public IQueryable<PrescriptionMedicine> GetMedicinesOfPrescription(int prescriptionID)
        {
            return _context.PrescriptionMedicines
                .AsNoTracking()
                .Where(pm => pm.PrescriptionId == prescriptionID)
                .Include(pm => pm.Medicine);
        }

        public async Task<PrescriptionMedicine> GetPrescriptionMedicine(int prescriptionID, int medicineID)
        {
            return await _context.PrescriptionMedicines
                .Include(pm => pm.Medicine)
                .FirstOrDefaultAsync(pm => pm.MedicineId == medicineID && pm.PrescriptionId == prescriptionID);
                
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePrescriptionMedicine(PrescriptionMedicine prescriptionMedicine)
        {
            await _context.PrescriptionMedicines
                .Where(pm => pm.PrescriptionId == prescriptionMedicine.PrescriptionId && pm.MedicineId == prescriptionMedicine.MedicineId)
                .ExecuteUpdateAsync(pm => pm
                .SetProperty(e => e.Dosage, n => prescriptionMedicine.Dosage)
                .SetProperty(e => e.Duration, n => prescriptionMedicine.Duration));
        }
    }
}
