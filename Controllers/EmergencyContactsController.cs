using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System.Threading.Tasks;

namespace Hospital_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmergencyContactsController : ControllerBase
    {
        private readonly IEmergencyContactServices _emergencyContactService;
        private readonly ILogger<EmergencyContactsController> _logger;

        public EmergencyContactsController(IEmergencyContactServices emergencyContactService,
            ILogger<EmergencyContactsController> logger)
        {
            _emergencyContactService = emergencyContactService;
            _logger = logger;
        }


        [HttpGet]
        public IActionResult GetAllPatientEmergencyContacts(int patientId)
        {
            try
            {
                _logger.LogInformation("Retrieving emergency contacts for patient with ID {PatientId}", patientId);
                var emergencyContacts = _emergencyContactService.GetAllPatientEmergencyContactsAsync(patientId);
                if (emergencyContacts == null || !emergencyContacts.Any())
                {
                    _logger.LogWarning("No emergency contacts found for patient with ID {PatientId}", patientId);
                    return NotFound($"No emergency contacts found for patient with ID {patientId}");
                }
                return Ok(emergencyContacts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving emergency contacts.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var EContact = await _emergencyContactService.GetById(id);
            return Ok(EContact);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmergencyContact([FromBody] EmergencyContactCreationDto emergencyContact)
        {
            if (emergencyContact == null)
            {
                _logger.LogWarning("Received null emergency contact data.");
                return BadRequest("Emergency contact data is required.");
            }
            try
            {
                await _emergencyContactService.AddAsync(emergencyContact);
                _logger.LogInformation("Emergency contact added successfully for patient with ID {PatientId}", emergencyContact.PatientId);
                return CreatedAtAction(nameof(GetAllPatientEmergencyContacts), new { patientId = emergencyContact.PatientId }, emergencyContact);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding the emergency contact.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpDelete("Delete-All-Patient-Emergency-Contacts")]
        public async Task<IActionResult> DeleteAllPatientEmergencyContacts(int patientId)
        {
            await _emergencyContactService.DeleteAllPatientEmergencyContacts(patientId);
            return Ok("deleted!");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _emergencyContactService.DeleteAsync(id);
            return Ok("Deleted!");
        }
    
    }
}
