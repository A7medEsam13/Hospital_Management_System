
namespace Hospital_Management_System.Repository
{
    public class PrescriptionRepository : IPrescriptionRepository
    {
        private readonly ApplicationDbContext _context;

        public PrescriptionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateNewPrescriptionAsync(Prescription prescription)
        {
            await _context.Prescriptions.AddAsync(prescription);
        }

        public async Task DeletePrescriptionAsync(int prescriptionID)
        {
            await _context.Prescriptions
                .Where(p => p.Id == prescriptionID)
                .ExecuteDeleteAsync();
        }

        public List<Prescription> GetAllDoctorPrescriptions(string doctorSSN)
        {
            var prescriptions = _context.Prescriptions
                .AsNoTracking()
                .Where(p => p.DoctorId == doctorSSN)
                .ToList();
            return prescriptions;
        }

        public List<Prescription> GetAllPatientPrescriptions(int patientID)
        {
            return _context.Prescriptions
                .AsNoTracking()
                .Where(p => p.PatientId == patientID)
                .ToList();
        }

        public async Task<List<LaboratoryScreeningPrescription>> GetAllPrescriptionScreenings(int prescriptionID)
        {
            var screenings = await _context.Set<LaboratoryScreeningPrescription>()
                .Where(s => s.PrescriptionId == prescriptionID)
                .Include(s=>s.LaboratoryScreening)
                .ToListAsync();
            return screenings;
        }

        public async Task<List<Prescription>> GetUnPaidPrescriptions(int patientID)
        {
            return await _context.Prescriptions
                .AsNoTracking()
                .Where(p => !p.IsPaid)
                .ToListAsync();
        }

        public async Task Pay(Prescription prescription)
        {
            await _context.Prescriptions
                .Where(p => p.Id == prescription.Id)
                .ExecuteUpdateAsync(s => s
                .SetProperty(o => o.IsPaid, n => true));
        }
    }
}
