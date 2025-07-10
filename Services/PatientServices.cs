using Hospital_Management_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Management_System.Services
{
    public class PatientServices : IPatientServices
    {
        private readonly ApplicationDbContext _context;
        public PatientServices(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddPatient(Patient patient)
        {
            await _context.Patients.AddAsync(patient);
        }

        public async Task<ICollection<Patient>> GetAllPatients()
        {
            var patients = await _context.Patients.ToListAsync();
            return patients;
        }

        public Patient GetPatientById(int patientId)
        {
            var patient = _context.Patients.FirstOrDefault(p=>p.Id == patientId);
            return patient;
        }

        public Task<Patient> GetPatientByName(string name)
        {
            name = name.ToLower();
            return _context.Patients
                .FirstOrDefaultAsync(p => 
                (p.FirstName + " " + p.LastName).ToLower() == name);
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
