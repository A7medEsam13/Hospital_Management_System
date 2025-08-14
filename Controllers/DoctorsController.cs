using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Hospital_Management_System.Controllers
{
    /// <summary>
    /// displaying patients records
    /// Diagnosis Cases
    /// Writing Prescriptions
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly ILogger<DoctorsController> _logger;
        private readonly IDoctorServices _doctorServices;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DoctorsController(
            ILogger<DoctorsController> logger,
            IDoctorServices doctorServices,
            IMapper mapper,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _doctorServices = doctorServices;
            _mapper = mapper;
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

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for doctor data.");
                return BadRequest(ModelState);
            }
            await _doctorServices.Add(dto);
            return Created();



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

        [HttpGet("by-id")]
        public  IActionResult GetDoctorById(string id)
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

        [HttpPut]
        public IActionResult UpdateDoctor(string id, DoctorUpdateDto dto)
        {
            if (dto == null)
            {
                _logger.LogError("Doctor data is null.");
                return BadRequest("Invalid doctor data.");
            }
            
            _doctorServices.Update(dto);
            _logger.LogInformation("Doctor with ID {DoctorId} updated successfully.", id);
            return Ok("Doctor updated successfully.");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDoctor(string id)
        {
            var doctor = _doctorServices.GetById(id);
            if (doctor == null)
            {
                _logger.LogError("Doctor with ID {DoctorId} not found.", id);
                return NotFound($"Doctor with ID {id} not found.");
            }
            _logger.LogInformation("Doctor with ID {DoctorId} has been terminated successfully.", id);
            return Ok("Doctor deleted successfully.");
        }
    }
}
