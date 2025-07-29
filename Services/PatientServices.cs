using Hospital_Management_System.Models;
using Hospital_Management_System.Repository;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Hospital_Management_System.Services
{
    public class PatientServices : IPatientServices
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;

        public PatientServices(IPatientRepository patientRepository,
            IMapper mapper)
        {
            _patientRepository = patientRepository;
            _mapper = mapper;
        }

        public async Task AddPatient(PatientCreationDto patientDto)
        {
            var patient = _mapper.Map<Patient>(patientDto);
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

        public  IEnumerable<Patient> GetPatientByName(string name)
        {
            name =name.ToLower();
            return  _patientRepository.GetPatientsByName(name);
        }

        public async Task RemovePatient(int patientId)
        {
            await _patientRepository.RemovePatient(patientId);
            await _patientRepository.SaveAsync();

        }

        public async Task Updatepatient(Patient patient)
        {
            _patientRepository.Updatepatient(patient);
            await _patientRepository.SaveAsync(); // Ensure the save operation completes
        }

        public Task Updatepatient(PatientUpdateDto patient)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Patient>> IPatientServices.GetAllPatients()
        {
            throw new NotImplementedException();
        }

        Task<Patient> IPatientServices.GetPatientByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
