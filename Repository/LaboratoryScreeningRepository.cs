
namespace Hospital_Management_System.Repository
{
    public class LaboratoryScreeningRepository(ApplicationDbContext context) : ILaboratoryScreeningRepository
    {
        private readonly ApplicationDbContext _context = context;
        public async Task CreateScreening(LaboratoryScreening screening)
        {
            await _context.LaboratoryScreenings.AddAsync(screening);
        }

        public async Task DeleteScreening(int id)
        {
            await _context.LaboratoryScreenings
                .Where(s => s.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<List<LaboratoryScreening>> GetAllDoctorScreenings(string doctorSSN)
        {
            return await _context.LaboratoryScreenings
                .AsNoTracking()
                .Where(s => s.DoctorId == doctorSSN)
                .ToListAsync();
        }

        public async Task<List<LaboratoryScreening>> GetAllPatientScreenings(int patientID)
        {
            return await _context.LaboratoryScreenings
                .AsNoTracking()
                .Where(s => s.PatientId == patientID)
                .ToListAsync();
        }

        public async Task<List<LaboratoryScreening>> GetAllTechnicanScreenings(string technicanSSN)
        {
            return await _context.LaboratoryScreenings
                .AsNoTracking()
                .Where(s => s.TechnicianSSN == technicanSSN)
                .ToListAsync();
        }


        public async Task<LaboratoryScreening> GetByID(int id)
        {
            return await _context.LaboratoryScreenings
                .FindAsync(id);
        }

        public async Task<LaboratoryScreening> GetScreeningByPatientIDAndDoctorSSN(int patientID, string doctorSSN)
        {
            return await _context.LaboratoryScreenings
                .FindAsync(patientID, doctorSSN);
        }

       

        public async Task UpdateScreening(LaboratoryScreening screening)
        {
            await _context.LaboratoryScreenings
                .Where(s => s.Id == screening.Id)
                .ExecuteUpdateAsync(s => s
                .SetProperty(o => o.Report, n => screening.Report));
        }
    }
}
