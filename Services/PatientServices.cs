using Hospital_Management_System.Models;
using Hospital_Management_System.Repository;
using Hospital_Management_System.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Hospital_Management_System.Services
{
    public class PatientServices : IPatientServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PatientServices(IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task AddPatient(PatientCreationDto patientDto)
        {
            var patient = _mapper.Map<Patient>(patientDto);
            await _unitOfWork.Patients.AddPatient(patient);
            await _unitOfWork.Complete();
        }

        

        public  IEnumerable<Patient> GetAllPatients()
        {
            return _unitOfWork.Patients.GetAllPatients().AsEnumerable();
        }

        public async Task<Patient> GetPatientById(int patientId)
        {
            var patient = await _unitOfWork.Patients.GetPatientById(patientId);
            if (patient == null)
            {
                throw new KeyNotFoundException($"Patient with ID {patientId} not found.");
            }
            return patient;
        }

        public  IEnumerable<Patient> GetPatientByName(string name)
        {
            name =name.ToLower();
            return _unitOfWork.Patients.GetPatientsByName(name);
        }

        public async Task<string> GetPatientFullName(int id)
        {
            var patient = await _unitOfWork.Patients.GetPatientById(id);
            return patient.FirstName + " " + patient.LastName;
        }

        public async Task RemovePatient(int patientId)
        {
            await _unitOfWork.Patients.RemovePatient(patientId);
            await _unitOfWork.Complete();

        }

        public async Task Updatepatient(PatientUpdateDto dto)
        {
            var patient = _mapper.Map<Patient>(dto);
            _unitOfWork.Patients.Updatepatient(patient);
            await _unitOfWork.Complete();// Ensure the save operation completes
        }
        IEnumerable<Patient> IPatientServices.GetPatientByName(string name)
        {
            return _unitOfWork.Patients.GetPatientsByName(name);
        }
    }
}
