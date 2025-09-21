
namespace Hospital_Management_System.Repository
{
    public class DiagnosisPatientRepository : IDiagnosisPatientRepository
    {
        private readonly ApplicationDbContext _context;

        public DiagnosisPatientRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(DiagnosisPatient diagnosisPatient)
        {
            await _context.DiagnosisPatient.AddAsync(diagnosisPatient);
        }

        public DiagnosisPatient GetByDiagnosisId(int diagnosisId)
        {
            var diagnosisPatient =  _context.DiagnosisPatient.First(dp => dp.DiagnosisId == diagnosisId);
            return diagnosisPatient;
        }

        public async Task<int> GetPatientID(int diagnosisID)
        {
            var diagnosisPatient = await _context.DiagnosisPatient.FirstOrDefaultAsync(dp => dp.DiagnosisId == diagnosisID);
            return diagnosisPatient.PatientId;
        }
    }
}
