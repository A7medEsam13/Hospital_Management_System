using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Hospital_Management_System.Controllers
{
    [Authorize(Roles = "Admin")]
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
        public  async Task<IActionResult> GetDoctorById(string id)
        {
            var doctor = await _doctorServices.GetById(id);
            if (doctor == null)
            {
                _logger.LogWarning("Doctor with ID {DoctorId} not found.", id);
                return NotFound($"Doctor with ID {id} not found.");
            }
            return Ok(doctor);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDoctor(DoctorUpdateDto dto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (dto == null)
            {
                _logger.LogError("Doctor data is null.");
                return BadRequest("Invalid doctor data.");
            }

            await _doctorServices.Update(dto);
            _logger.LogInformation("Doctor with ID {DoctorId} updated successfully.", dto.SSN);
            return Ok("Doctor updated successfully.");
        }

        
    }
}
