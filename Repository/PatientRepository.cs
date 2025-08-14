using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Hospital_Management_System.Repository
{
    public class PatientRepository : IPatientRepository
    {
        private readonly ApplicationDbContext _context;

        public PatientRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddPatient(Patient patient)
        {
            await _context.Patients.AddAsync(patient);
        }

        public IQueryable<Patient> GetAllPatients()
        {
            var patients = _context.Patients
                .AsNoTracking();
            return patients;
        }


        public async Task<Patient> GetPatientById(int patientId)
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(p => p.Id == patientId);
                
            return patient;
        }

        public IEnumerable<Patient> GetPatientsByName(string name)
        {
            return _context.Patients
                .Where(p => (p.FirstName + " " + p.LastName).ToLower() == name);
        }

        public async Task RemovePatient(int patientId)
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(p => p.Id == patientId);
            _context.Patients.Remove(patient);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Updatepatient(Patient patient)
        {
            _context.Patients.Update(patient);
        }
    }
}
