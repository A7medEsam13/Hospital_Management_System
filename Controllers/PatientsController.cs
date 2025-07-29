using AutoMapper;
using Hospital_Management_System.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management_System.Controllers
{

    /// <summary>
    /// book appointments
    /// following the medical records
    /// show bills
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly ILogger<PatientsController> _logger;
        private readonly IMapper _mapper;
        private readonly IPatientServices _patientServices;
        public PatientsController(
            ILogger<PatientsController> logger,
            IPatientServices patientServices,
            IMapper mapper)
        {
            _logger = logger;
            _patientServices = patientServices;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddPatient([FromBody] PatientCreationDto patientDto)
        {
            if (patientDto == null)
            {
                // Log the error
                _logger.LogError("Patient data is null.");
                return BadRequest("Patient data is null.");
            }
            await _patientServices.AddPatient(patientDto);
            // log the success
            _logger.LogInformation("Patient added successfully.");
            return Created();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> RemovePatient(int id)
        {
            var patient = _patientServices.GetPatientById(id);
            if (patient == null)
            {
                // Log the error
                _logger.LogError($"Patient with ID {id} not found.");
                return NotFound($"Patient with ID {id} not found.");
            }
            await _patientServices.RemovePatient(id);
            // log the success
            _logger.LogInformation($"Patient with ID {id} removed successfully.");
            return Ok($"Patient with ID {id} removed successfully.");
        }
    

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetPatientById(int id)
        {
            var patient = _patientServices.GetPatientById(id);
            if (patient == null)
            {
                // Log the error
                _logger.LogError($"Patient with ID {id} not found.");
                return NotFound($"Patient with ID {id} not found.");
            }
            // log the success
            var patientDto = _mapper.Map<PatientCreationDto>(patient);
            _logger.LogInformation($"Patient with ID {id} retrieved successfully.");
            return Ok(patientDto);
        }

        [HttpGet("{name:alpha}")]
        public async Task<IActionResult> GetPatientByName(string name)
        {
            var patient = await _patientServices.GetPatientByName(name);
            if (patient == null)
            {
                // Log the error
                _logger.LogError($"Patient with name {name} not found.");
                return NotFound($"Patient with name {name} not found.");
            }
            // log the success
            _logger.LogInformation($"Patient with name {name} retrieved successfully.");
            var patientDto = _mapper.Map<PatientCreationDto>(patient);
            return Ok(patientDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPatients()
        {
            var patients = await _patientServices.GetAllPatients();
            if (patients == null || !patients.Any())
            {
                // Log the error
                _logger.LogError("No patients found.");
                return NotFound("No patients found.");
            }
            // log the success
            _logger.LogInformation("All patients retrieved successfully.");
            var patientDtos = _mapper.Map<IEnumerable<PatientCreationDto>>(patients);
            return Ok(patientDtos);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePatient([FromBody] PatientUpdateDto patientDto)
        {
            if (patientDto == null)
            {
                // Log the error
                _logger.LogError("Patient data is null.");
                return BadRequest("Patient data is null.");
            }
            _patientServices.Updatepatient(patientDto);
            // log the success
            _logger.LogInformation($"Patient with ID {patientDto.Id} updated successfully.");
            return Ok();
        }
    }
}