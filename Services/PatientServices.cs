using Hospital_Management_System.Models;
using Hospital_Management_System.Repository;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Hospital_Management_System.Services
{
    public class PatientServices : IPatientServices
    {
        private readonly IPatientRepository _patientRepository;

        public PatientServices(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task AddPatient(Patient patient)
        {
            await _patientRepository.AddPatient(patient);
            await _patientRepository.SaveAsync();
        }

        public async Task<ICollection<Patient>> GetAllPatients()
        {
            return await _patientRepository.GetAllPatients();
        }

        public async Task<Patient> GetPatientById(int patientId)
        {
            var patient = await _patientRepository.GetPatientById(patientId);
            if (patient == null)
            {
                throw new KeyNotFoundException($"Patient with ID {patientId} not found.");
            }
            return patient;
        }

        public async Task<Patient> GetPatientByName(string name)
        {
            return await _patientRepository.GetPatientByName(name);
        }

        public async Task RemovePatient(int patientId)
        {
            await _patientRepository.RemovePatient(patientId);
            await _patientRepository.SaveAsync();

        }

        public void Updatepatient(Patient patient)
        {
            _patientRepository.Updatepatient(patient);
            _patientRepository.SaveAsync().Wait(); // Ensure the save operation completes
        }
    }
}
