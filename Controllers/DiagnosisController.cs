using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Hospital_Management_System.Controllers
{
    [Authorize("Docotor")]
    [Route("api/[controller]")]
    [ApiController]
    public class DiagnosisController : ControllerBase
    {
        private readonly ILogger<DiagnosisController> _logger;
        private readonly IDiagnosisServices _diagnosisServices;

        public DiagnosisController(ILogger<DiagnosisController> logger,
            IDiagnosisServices diagnosisServices)
        {
            _logger = logger;
            _diagnosisServices = diagnosisServices;
        }


        [HttpGet]
        public  IActionResult GetAllDiagnosis()
        {
            _logger.LogInformation("Retrieving all diagnoses");
            var diagnoses = _diagnosisServices.GetAll();
            if (diagnoses == null)
            {
                _logger.LogWarning("No diagnoses found");
                return NotFound("No diagnoses found");
            }
            _logger.LogInformation("Retrieved diagnoses");
            return Ok(diagnoses);
        }

        [HttpPost]
        public async Task<IActionResult> AddDiagnosis([FromBody] DiagnosisCreationDto diagnosis)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for adding diagnosis");
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Adding a new diagnosis");
            await _diagnosisServices.AddAsync(diagnosis);
            _logger.LogInformation("Diagnosis added successfully");
            return Created();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteDiagnosis(int id)
        {
            _logger.LogInformation("Deleting diagnosis with id {Id}", id);
            try
            {
                await _diagnosisServices.DeleteAsync(id);
                _logger.LogInformation("Diagnosis with id {Id} deleted successfully", id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, "Diagnosis with id {Id} not found", id);
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateDiagnosis(int id, [FromBody] DiagnosisUpdateDto diagnosis)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for updating diagnosis");
                return BadRequest(ModelState);
            }
            _logger.LogInformation("Updating diagnosis with id {Id}", id);
            await _diagnosisServices.UpdateAsync(id, diagnosis);
            _logger.LogInformation("Diagnosis with id {Id} updated successfully", id);
            return Ok("Updated successfully!");
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation("Getting Dignosis With ID {id} from services", id);
            var diagnosis = await _diagnosisServices.GetByIdAsync(id);

            return Ok(diagnosis);
        }

    }
}
