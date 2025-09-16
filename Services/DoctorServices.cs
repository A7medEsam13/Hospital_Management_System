

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

            var user = await _userManager.FindByNameAsync(doctor.UserName);
            if (user == null)
            {
                _logger.LogError("User with username {UserName} not found.", doctor.UserName);
                throw new KeyNotFoundException($"User with username {doctor.UserName} not found.");
            }
            doctorEntity.User = user;
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
            _logger.LogInformation("Retrieved all doctors successfully.");
            return doctors;
        }

        public async Task<Doctor> GetById(string id)
        {
            var doctor = await _unitOfWork.Doctors.GetById(id);
            if(doctor == null)
            {
                _logger.LogError($"Doctor with SSN: {id} not found.");
                throw new KeyNotFoundException($"Doctor with SSN: {id} not found.");
            }
            _logger.LogInformation($"Doctor with SSN: {id} retrieved successfully.");
            return doctor;
        }

        public async Task Update(DoctorUpdateDto doctor)
        {
            var doctorFromDb = await _unitOfWork.Doctors.GetById(doctor.SSN);
            _unitOfWork.Doctors.Update(doctor);
            await _unitOfWork.Complete();

            _logger.LogInformation($"Doctor with SSN: {doctor.SSN} updated successfully.");
        }
    }
}
