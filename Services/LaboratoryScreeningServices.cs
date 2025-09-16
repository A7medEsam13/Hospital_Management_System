
using Hospital_Management_System.Repository;
using Hospital_Management_System.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hospital_Management_System.Services
{
    public class LaboratoryScreeningServices(IUnitOfWork unitOfWork, 
        ILogger<LaboratoryScreeningServices> logger,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor) : ILaboratoryScreeningServices
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ILogger<LaboratoryScreeningServices> _logger = logger;
        private readonly IMapper _mapper = mapper;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public async Task CreateLaboratoryScreening(LaboratoryScreeningCreationDto dto)
        {
            if(dto is null)
            {
                _logger.LogInformation("Model is invalid");
                throw new ArgumentNullException();
            }
            _logger.LogInformation("Mapping the dto to entity");
            var screeningEntity = _mapper.Map<LaboratoryScreening>(dto);

            var userID = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(userID is null)
            {
                _logger.LogError("ther is no stuff exists");
                throw new Exception("ther is no stuff exists");
            }
            var stuff = await _unitOfWork.Stuffs.GetStuffByUserID(userID);
            screeningEntity.TechnicianSSN = stuff.SSN;

            _logger.LogInformation("Creating the new screening And saving it to database");
            await _unitOfWork.LaboratoryScreenings.CreateScreening(screeningEntity);
            await _unitOfWork.Complete();
        }

        public async Task Delete(int id)
        {
            if (id <= 0)
            {
                _logger.LogError("Invalid Id");
                throw new Exception("Invalid ID");
            }

            _logger.LogInformation("Screening with ID {id} has been deleted successfully", id);
            await _unitOfWork.LaboratoryScreenings.DeleteScreening(id);
        }

        public async Task<List<LaboratoryScreeningDisplayDto>> GetAllDoctorScreenings(string doctorSSN)
        {
            var doctor = await _unitOfWork.Doctors.GetById(doctorSSN);

            if(doctor is null)
            {
                _logger.LogError(new NullReferenceException(), "doctor with SSN {ssn} not found", doctorSSN);
            }

            var screenings = await _unitOfWork.LaboratoryScreenings.GetAllDoctorScreenings(doctorSSN);
            if(screenings is null || screenings.Count == 0)
            {
                _logger.LogWarning("doctor with ssn {ssn} does not have any screenings", doctorSSN);
                return Enumerable.Empty<LaboratoryScreeningDisplayDto>().ToList();
            }


            _logger.LogInformation("Mapping the entities to dtos");
            var screeningDtos = _mapper.Map<List<LaboratoryScreeningDisplayDto>>(screenings);
            return screeningDtos;
        }

        public async Task<List<LaboratoryScreeningDisplayDto>> GetAllPatientScreenings(int patientID)
        {
            if (patientID <= 0)
            {
                _logger.LogError(new Exception("Invalid patient ID"), "Invalid patient ID");
            }

            _logger.LogInformation("Getting all patient's screenings that has ID {id}", patientID);
            var screenings = await _unitOfWork.LaboratoryScreenings.GetAllPatientScreenings(patientID);

            if(screenings is null || screenings.Count == 0)
            {
                _logger.LogWarning("patient with ID {id} does not have any screenings", patientID);
                return Enumerable.Empty<LaboratoryScreeningDisplayDto>().ToList();
            }

            _logger.LogInformation("mapping the entities to dtos");
            var screeningDtos = _mapper.Map<List<LaboratoryScreeningDisplayDto>>(screenings);
            return screeningDtos;
        }

        public async Task<List<LaboratoryScreeningDisplayDto>> GetAllTechnicanScreenings(string technicanSSN)
        {
            if (technicanSSN.Length < 16 || technicanSSN.Length > 16)
            {
                _logger.LogError(new Exception("Invalid SSN"), "Invalid SSN");
            }

            _logger.LogInformation("Getting all technican's screenigs that have ssn {ssn}", technicanSSN);
            var screenings = await _unitOfWork.LaboratoryScreenings.GetAllTechnicanScreenings(technicanSSN);

            if(screenings is null || screenings.Count == 0)
            {
                _logger.LogWarning("this technican {ssn} does not have made any screenings", technicanSSN);
            }


            _logger.LogInformation("Mapping the entities to dtos");
            var screeningDtos = _mapper.Map<List<LaboratoryScreeningDisplayDto>>(screenings);
            return screeningDtos;
        }

        public async Task<LaboratoryScreeningDisplayDto> GetByID(int id)
        {
            if (id <= 0)
            {
                _logger.LogError(new Exception("Invalid ID"), "Invalid ID");
            }

            _logger.LogInformation("Getting the screenig with ID {id} From database", id);
            var screening = await _unitOfWork.LaboratoryScreenings.GetByID(id);

            if(screening is null)
            {
                _logger.LogWarning("There is no screening exists with this ID");
            }

            _logger.LogInformation("Mapping the entity to dto");
            var dto = _mapper.Map<LaboratoryScreeningDisplayDto>(screening);
            return dto;
        }

        public async Task<LaboratoryScreeningDisplayDto> GetScreeningByPatientIDAndDooctorSSN(int patientID, string doctorSSN)
        {
            if (patientID <= 0 || doctorSSN.Length < 16 || doctorSSN.Length > 16)
            {
                _logger.LogError(new InvalidDataException(), "Invalid data");
            }


            _logger.LogInformation("Getting the screening from database");
            var screening = await _unitOfWork.LaboratoryScreenings.GetScreeningByPatientIDAndDoctorSSN(patientID, doctorSSN);

            if(screening is null)
            {
                _logger.LogWarning("There is no screening exists with Patient ID {id} and Doctor SSN {ssn}", patientID, doctorSSN);
            }


            _logger.LogInformation("Mapping the entity to dto");
            var dto = _mapper.Map<LaboratoryScreeningDisplayDto>(screening);
            return dto;
        }


        public async Task Update(LaboratoryScreeningUpdateDto dto)
        {
            var updatedEntity = _mapper.Map<LaboratoryScreening>(dto);

            _logger.LogInformation("Updating the screening with ID {id}", dto.ID);
            await _unitOfWork.LaboratoryScreenings.UpdateScreening(updatedEntity);
        }
    }
}
