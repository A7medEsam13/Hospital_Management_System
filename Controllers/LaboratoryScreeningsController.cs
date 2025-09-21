using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management_System.Controllers
{
    [Authorize(Roles = "Technican")]
    [Route("api/[controller]")]
    [ApiController]
    public class LaboratoryScreeningsController(ILaboratoryScreeningServices laboratoryScreeningServices,
        ILogger<LaboratoryScreeningsController> logger) : ControllerBase
    {
        private readonly ILaboratoryScreeningServices _laboratoryScreeningServices = laboratoryScreeningServices;
        private readonly ILogger<LaboratoryScreeningsController> _logger = logger;


        [HttpPost]
        public async Task<IActionResult> CreateNewRepository(LaboratoryScreeningCreationDto dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid Model");
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Calling the service to create");
            await _laboratoryScreeningServices.CreateLaboratoryScreening(dto);
            return Created();
        }


        [HttpGet("Doctor")]
        public async Task<IActionResult> GetAllDoctorScreening(string doctorSSN)
        {
            _logger.LogInformation("Getting all screenings of docotor {ssn}", doctorSSN);
            var screenings = await _laboratoryScreeningServices.GetAllDoctorScreenings(doctorSSN);
            return Ok(screenings);
        }

        [HttpGet("patient")]
        public async Task<IActionResult> GetAllPatientScreenings(int patientID)
        {
            _logger.LogInformation("Getting all screenings of patient {id}", patientID);
            var screenings = await _laboratoryScreeningServices.GetAllPatientScreenings(patientID);

            return Ok(screenings);
        }

        [HttpGet("technican")]
        public async Task<IActionResult> GetAllTechnicanScreenings(string technicanSSN)
        {
            _logger.LogInformation("Getting all screenings of technican {ssn}", technicanSSN);
            var screenigs = await _laboratoryScreeningServices.GetAllTechnicanScreenings(technicanSSN);

            return Ok(screenigs);
        }


        [HttpGet]
        public async Task<IActionResult> GetScreeningByPatientIDAndDoctorSSN(int patientID,string doctorSSN)
        {
            var screening = await _laboratoryScreeningServices.GetScreeningByPatientIDAndDooctorSSN(patientID, doctorSSN);

            return Ok(screening);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByID(int id)
        {
            _logger.LogInformation("Getting the screening with id {id}", id);
            var screening = await _laboratoryScreeningServices.GetByID(id);

            return Ok(screening);
        }

        [HttpPut]
        public async Task<IActionResult> Update(LaboratoryScreeningUpdateDto dto)
        {
            _logger.LogInformation("Updating screening that has id {id}", dto.ID);
            await _laboratoryScreeningServices.Update(dto);

            return Ok("Updated");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Deleting screening with ID {id}", id);
            await _laboratoryScreeningServices.Delete(id);

            return Ok("deleted");
        }
    }
}
