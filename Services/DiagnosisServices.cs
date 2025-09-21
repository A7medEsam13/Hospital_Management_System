
using Hospital_Management_System.Extensions;
using Hospital_Management_System.Repository;
using Hospital_Management_System.UnitOfWork;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Hospital_Management_System.Services
{
    public class DiagnosisServices : IDiagnosisServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DiagnosisServices> _logger;
        private readonly IMapper _mapper;
        private readonly IPatientServices _patientServices;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DiagnosisServices(ILogger<DiagnosisServices> logger,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IPatientServices patientServices,
            IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _patientServices = patientServices;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task AddAsync(DiagnosisCreationDto diagnosisDto)
        {
            _logger.LogInformation("Mapping the diagnosis dto");
            var diagnosis = _mapper.Map<Diagnosis>(diagnosisDto);

            var doctorData = await GetData.GetCurrentUserDataAsync(_httpContextAccessor, _unitOfWork);

            diagnosis.DoctorSSN = doctorData.SSN;

            _logger.LogInformation("Adding the diagnosis to the repository");
            await _unitOfWork.Diagnoses.AddAsync(diagnosis);
            await _unitOfWork.Complete();

            var diagnosisFromDB =  _unitOfWork.Diagnoses.GetAll()
                .FirstOrDefault(d => d.Name == diagnosisDto.Name && d.DoctorSSN == doctorData.SSN);
            var diagnosisPatient = new DiagnosisPatient
            {
                DiagnosisId = diagnosisFromDB.Id,
                PatientId = diagnosisDto.PatientId
            };
            _logger.LogInformation("Adding the diagnosis patient to the repository");
            await _unitOfWork.DiagnosisPatient.AddAsync(diagnosisPatient);
            await _unitOfWork.Complete();
        }

        public async Task DeleteAsync(int id)
        {
            _logger.LogInformation("Getting the diagnosis by id from database");
            var diagnosis = await _unitOfWork.Diagnoses.GetByIdAsync(id);
            if(diagnosis == null)
            {
                _logger.LogError("Diagnosis with id {Id} not found", id);
                throw new KeyNotFoundException($"Diagnosis with id {id} not found.");
            }
            _logger.LogInformation("Deleting the diagnosis from the repository");
            await _unitOfWork.Diagnoses.DeleteAsync(diagnosis);
            _logger.LogInformation("Diagnosis Deleted successfully");
        }

        public async Task<IEnumerable<DiagnosisDisplayDto>> GetAll()
        {
            _logger.LogInformation("Retrieving all diagnoses from the repository");
            var diagnoses = _unitOfWork.Diagnoses.GetAll().AsEnumerable();
            if (diagnoses == null)
            {
                _logger.LogWarning("No diagnoses found in the repository");
                return Enumerable.Empty<DiagnosisDisplayDto>();
            }
            _logger.LogInformation("Mapping diagnoses to display DTOs");
            return await Task.WhenAll( diagnoses
                .Select(async d => new DiagnosisDisplayDto
                {
                    Id = d.Id,
                    Name = d.Name,
                    Details = d.Details,
                    DoctorSSN = d.DoctorSSN,
                    DoctorName = d.Doctor.FirstName + " " + d.Doctor.LastName,
                    patientId = _unitOfWork.DiagnosisPatient.GetByDiagnosisId(d.Id).PatientId,
                    PatientName = await _patientServices.GetPatientFullName(_unitOfWork.DiagnosisPatient.GetByDiagnosisId(d.Id).PatientId)
                }));
        }

        public  IEnumerable<Task<DiagnosisDisplayDto>> GetAllDoctorDiagnosis(string doctorSSN)
        {
            _logger.LogInformation("Retrieving all diagnoses for doctor with SSN {DoctorSSN}", doctorSSN);
            var diagnoses = _unitOfWork.Diagnoses.GetAllDoctorDiagnosis(doctorSSN);
            if (diagnoses == null || !diagnoses.Any())
            {
                _logger.LogWarning("No diagnoses found for doctor with SSN {DoctorSSN}", doctorSSN);
                return Enumerable.Empty<Task<DiagnosisDisplayDto>>();
            }
            _logger.LogInformation("Mapping diagnoses to display DTOs for doctor");
            
            var diagnosisDtos = diagnoses
                .Select(async d => new DiagnosisDisplayDto
                {
                    Id = d.Id,
                    Name = d.Name,
                    Details = d.Details,
                    DoctorSSN = d.DoctorSSN,
                    DoctorName = d.Doctor.FirstName + " " + d.Doctor.LastName,
                    patientId = await _unitOfWork.DiagnosisPatient.GetPatientID(d.Id),
                    PatientName = await _patientServices.GetPatientFullName(await _unitOfWork.DiagnosisPatient.GetPatientID(d.Id))
                });
            return diagnosisDtos;
        }

        public async Task<IEnumerable<DiagnosisDisplayDto>> GetAllPatientDiagnosis(int patientId)
        {
            if (patientId <= 0)
            {
                _logger.LogError("Invalid patient ID: {PatientId}", patientId);
                throw new ArgumentException("Patient ID must be greater than zero.", nameof(patientId));
            }
            _logger.LogInformation("Retrieving all diagnoses for patient with ID {PatientId}", patientId);
            var diagnoses = _unitOfWork.Diagnoses.GetAllPatientDiagnosis(patientId);
            if(diagnoses == null || !diagnoses.Any())
            {
                _logger.LogWarning("No diagnoses found for patient with ID {PatientId}", patientId);
                return Enumerable.Empty<DiagnosisDisplayDto>();
            }
            var patient = await _unitOfWork.Patients.GetPatientById(patientId);

            return diagnoses
                .Select(d => new DiagnosisDisplayDto
                {
                    Id = d.Id,
                    Name = d.Name,
                    Details = d.Details,
                    DoctorSSN = d.DoctorSSN,
                    DoctorName = d.Doctor.FirstName + " " + d.Doctor.LastName,
                    patientId = patientId,
                    PatientName = patient.FirstName + ' ' + patient.LastName
                });
        }

        public async Task<DiagnosisDisplayDto> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogError("Invalid diagnosis ID: {Id}", id);
                throw new ArgumentException("Diagnosis ID must be greater than zero.", nameof(id));
            }
            _logger.LogInformation("Retrieving diagnosis by ID: {Id}", id);
            var diagnosis = await _unitOfWork.Diagnoses.GetByIdAsync(id);

            if (diagnosis == null)
            {
                _logger.LogError("Diagnosis with ID {Id} not found", id);
                throw new KeyNotFoundException($"Diagnosis with ID {id} not found.");
            }
            var doctor = diagnosis.Doctor;
            var patientID = _unitOfWork.DiagnosisPatient.GetByDiagnosisId(diagnosis.Id).PatientId;
            var patient = await _unitOfWork.Patients.GetPatientById(patientID);
            return new DiagnosisDisplayDto
            {
                Id = diagnosis.Id,
                Name = diagnosis.Name,
                Details = diagnosis.Details,
                DoctorSSN = diagnosis.DoctorSSN,
                DoctorName = doctor.FirstName + ' ' + doctor.LastName,
                patientId = patientID,
                PatientName = patient.FirstName + ' ' + patient.LastName
            };
        }

        public async Task UpdateAsync(int id, DiagnosisUpdateDto diagnosis)
        {
            var existingDiagnosis = await _unitOfWork.Diagnoses.GetByIdAsync(id);
            if(existingDiagnosis == null)
            {
                _logger.LogError("Diagnosis with id {Id} not found", id);
                throw new KeyNotFoundException($"Diagnosis with id {id} not found.");
            }
            _logger.LogInformation("Mapping the diagnosis update dto");
            existingDiagnosis.Name = diagnosis.Name;
            existingDiagnosis.Details = diagnosis.Details;

            _logger.LogInformation("Updating the diagnosis with id {Id}", id);
            await _unitOfWork.Diagnoses.UpdateAsync(existingDiagnosis);
            await _unitOfWork.Complete();
            _logger.LogInformation("Diagnosis with id {Id} updated successfully", id);
        }

    }
}
