

using Hospital_Management_System.Repository;
using Hospital_Management_System.UnitOfWork;
using Microsoft.AspNetCore.Identity;

namespace Hospital_Management_System.Services
{
    public class DoctorServices : IDoctorServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<DoctorServices> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public DoctorServices(IMapper mapper,
            ILogger<DoctorServices> logger,
            UserManager<ApplicationUser> userManager,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _logger = logger;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public async Task Add(DoctorCreateDto doctor)
        {
            if (doctor == null)
            {
                _logger.LogError("Doctor object sent from client is null.");
                throw new ArgumentNullException(nameof(doctor));
            }
            var doctorEntity = _mapper.Map<Doctor>(doctor);

            doctorEntity.Role = "Doctor";
            
            await _unitOfWork.Doctors.Add(doctorEntity);
            await _unitOfWork.Complete();
            _logger.LogInformation($"Doctor with SSN: {doctor.SSN} added successfully.");
        }

        public async Task<IEnumerable<DoctorDisplayDto>> GetAll()
        {
            var doctors = await _unitOfWork.Doctors.GetAll();
            if(doctors == null || !doctors.Any())
            {
                _logger.LogError("No doctors found in the database.");
                return Enumerable.Empty<DoctorDisplayDto>();
            }

            var doctorsDto = _mapper.Map<IEnumerable<DoctorDisplayDto>>(doctors);
            _logger.LogInformation("Retrieved all doctors successfully.");
            return doctorsDto;
        }

        public async Task<DoctorDisplayDto> GetById(string id)
        {
            var doctor = await _unitOfWork.Doctors.GetById(id);
            if(doctor == null)
            {
                _logger.LogError($"Doctor with SSN: {id} not found.");
                throw new KeyNotFoundException($"Doctor with SSN: {id} not found.");
            }
            _logger.LogInformation($"Doctor with SSN: {id} retrieved successfully.");
            var doctorDto = _mapper.Map<DoctorDisplayDto>(doctor);
            return doctorDto;
        }

        public async Task Update(DoctorUpdateDto dto)
        {
            var doctor = await _unitOfWork.Doctors.GetById(dto.SSN);

            if (dto.Address != "string" && !string.IsNullOrEmpty(dto.Address))
            {
                doctor.Address = dto.Address;
            }
            if (dto.DepartmentName != "string" && !string.IsNullOrEmpty(dto.DepartmentName))
            {
                doctor.DepartmentName = dto.DepartmentName;
            }

            if (dto.Qualification != "string" && !string.IsNullOrEmpty(dto.Qualification))
            {
                doctor.Qualification = dto.Qualification;
            }

            if (dto.Specialization != "string" && !string.IsNullOrEmpty(dto.Specialization))
            {
                doctor.Specialization = dto.Specialization;
            }

            await _unitOfWork.Doctors.Update(doctor);
            await _unitOfWork.Complete();

            _logger.LogInformation($"Doctor with SSN: {doctor.SSN} updated successfully.");
        }

        
    }
}
