
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

        public IQueryable<Prescription> GetAllDoctorPrescriptions(string doctorSSN)
        {
            var prescriptions = _context.Prescriptions
                .AsNoTracking()
                .Where(p => p.DoctorId == doctorSSN);
            return prescriptions;
        }

        public IQueryable<Prescription> GetAllPatientPrescriptions(int patientID)
        {
            return _context.Prescriptions
                .AsNoTracking()
                .Where(p => p.PatientId == patientID);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        
    }
}
