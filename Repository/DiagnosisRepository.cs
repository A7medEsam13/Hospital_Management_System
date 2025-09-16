
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Hospital_Management_System.Repository
{
    public class DiagnosisRepository : IDiagnosisRepository
    {
        private readonly ApplicationDbContext _context;

        public DiagnosisRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Diagnosis diagnosis)
        {
            await _context.Diagnoses.AddAsync(diagnosis);
        }

        public async Task DeleteAsync(Diagnosis diagnosis)
        {
            await _context.Diagnoses
                .Where(d => d.Id == diagnosis.Id)
                .ExecuteDeleteAsync();
        }

        public List<Diagnosis> GetAll()
        {
            return _context.Diagnoses
                .AsNoTracking()
                .Include(d => d.Doctor)
                .Include(d => d.DiagnosisPatient)
                    .ThenInclude(pd => pd.Patient)
                    .ToList();
        }

        public List<Diagnosis> GetAllDoctorDiagnosis(string doctorSSN)
        {
            return _context.Diagnoses
                .AsNoTracking()
                .Where(d => d.DoctorSSN == doctorSSN)
                .Include(d => d.Doctor)
                .ToList();
        }

        public List<Diagnosis> GetAllPatientDiagnosis(int patientId)
        {
            return _context.DiagnosisPatient
                .AsNoTracking()
                .Where(pd => pd.PatientId == patientId)
                .Select(pd => pd.Diagnosis)
                .ToList();
                
        }

        public async Task<Diagnosis> GetByIdAsync(int id)
        {
            return await _context.Diagnoses
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == id);
               
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Diagnosis diagnosis)
        {
            await _context.Diagnoses
                .Where(d => d.Id == diagnosis.Id)
                .ExecuteUpdateAsync(setter =>
                setter
                .SetProperty(d => d.Details, d => diagnosis.Details)
                .SetProperty(d => d.Name, d => diagnosis.Name)
                .SetProperty(d => d.DoctorSSN, d => diagnosis.DoctorSSN));
        }
    }
}
