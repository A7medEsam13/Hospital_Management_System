using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Hospital_Management_System.Controllers
{
    /// <summary>
    /// displaying patients records
    /// Diagnosis Cases
    /// Writing Preciptions
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly ILogger<DoctorsController> _logger;
        private readonly IDoctorServices _doctorServices;
        private readonly IMapper _mapper;
        private readonly IStaffServices _staffServices;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DoctorsController(
            ILogger<DoctorsController> logger,
            IDoctorServices doctorServices,
            IMapper mapper,
            IStaffServices staffServices,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _doctorServices = doctorServices;
            _mapper = mapper;
            _staffServices = staffServices;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost]
        public async Task<IActionResult> AddDoctor(DoctorCreateDto dto)
        {
            if (dto == null)
            {
                _logger.LogError("Doctor data is null.");
                return BadRequest("Invalid doctor data.");
            }

            // mapping and adding the staff.
            var stuff = _mapper.Map<Stuff>(dto);
           
            var user = await _userManager.FindByIdAsync(stuff.UserId);

            IdentityResult result = await _userManager.AddToRoleAsync(user, "Doctor");

            await _staffServices.Add(stuff);
            _logger.LogInformation("Staff added successfully with SSN {StaffSSN}.", stuff.SSN);

            // mapping and adding the dioctor
            var doctor = _mapper.Map<Doctor>(dto);
            await _doctorServices.Add(doctor);
            await _doctorServices.SaveAsync();
            _logger.LogInformation("Doctor added successfully with ID {DoctorId}.", doctor.Id);
            return Ok("Doctor added successfully.");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDoctors()
        {
            var doctors = await _doctorServices.GetAll();
            if (doctors == null || !doctors.Any())
            {
                _logger.LogInformation("No doctors found.");
                return NotFound("No doctors found.");
            }
            var doctorDtos = _mapper.Map<IEnumerable<DoctorDisplayDto>>(doctors);
            return Ok(doctorDtos);
        }

        [HttpGet("{id:int}")]
        public  IActionResult GetDoctorById(int id)
        {
            var doctor = _doctorServices.GetById(id);
            if (doctor == null)
            {
                _logger.LogWarning("Doctor with ID {DoctorId} not found.", id);
                return NotFound($"Doctor with ID {id} not found.");
            }
            var doctorDto = _mapper.Map<DoctorDisplayDto>(doctor);
            return Ok(doctorDto);
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateDoctor(int id, DoctorUpdateDto dto)
        {
            if (dto == null)
            {
                _logger.LogError("Doctor data is null.");
                return BadRequest("Invalid doctor data.");
            }
            var doctor = _doctorServices.GetById(id);
            if (doctor == null)
            {
                _logger.LogWarning("Doctor with ID {DoctorId} not found.", id);
                return NotFound($"Doctor with ID {id} not found.");
            }
            _mapper.Map(dto, doctor);
            _doctorServices.Update(doctor);
            _doctorServices.SaveAsync();
            _logger.LogInformation("Doctor with ID {DoctorId} updated successfully.", id);
            return Ok("Doctor updated successfully.");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            var doctor = _doctorServices.GetById(id);
            if (doctor == null)
            {
                _logger.LogError("Doctor with ID {DoctorId} not found.", id);
                return NotFound($"Doctor with ID {id} not found.");
            }
            await _staffServices.Remove(doctor.Staff.SSN);
            await _doctorServices.SaveAsync();
            _logger.LogInformation("Doctor with ID {DoctorId} has been terminated successfully.", id);
            return Ok("Doctor deleted successfully.");
        }
    }
}
