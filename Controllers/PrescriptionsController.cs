using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management_System.Controllers
{
    [Authorize(Roles = "Doctor")]
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionsController : ControllerBase
    {
        private readonly IPrescriptionService _prescriptionService;
        private readonly ILogger<PrescriptionsController> _logger;
        public PrescriptionsController(IPrescriptionService prescriptionService, 
            ILogger<PrescriptionsController> logger)
        {
            _prescriptionService = prescriptionService;
            _logger = logger;
        }

        [HttpPost("newPrescription")]
        public async Task<IActionResult> CreateNewPrescription(PrescriptionCreationDto dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model");
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Creating the prescription");
            await _prescriptionService.CreateNewPrescription(dto);
            return Created();
        }

        [HttpGet("prescriptions")]
        public IActionResult GetAllPatientPrescriptions(int patientID)
        {
            _logger.LogInformation("Getting all prescriptions of patient {id}", patientID);
            var prescriptions = _prescriptionService.GetAllPatientPrescriptions(patientID);

            if (prescriptions.Count() == 0)
            {
                _logger.LogWarning("the patient {id} does not have any prescription", patientID);
                return Empty;
            }

            return Ok(prescriptions);
        }


        [HttpDelete("prescription")]
        public async Task<IActionResult> DeletePrescription(int prescriptionID)
        {
            _logger.LogInformation("Deleting prescription {id}", prescriptionID);
            await _prescriptionService.DeletePrescriptionAsync(prescriptionID);

            return Ok();
        }

        [HttpGet("medicines")]
        public IActionResult GetAllPrecriptionMedicines(int prescriptionID)
        {
            _logger.LogInformation("Getting all medicines of prescription {id}", prescriptionID);
            var medicines = _prescriptionService.GetAllPrescriptionMedicines(prescriptionID);

            if(medicines.Count() == 0)
            {
                _logger.LogWarning("presciption {id} does not has any medicine", prescriptionID);
                return Empty;
            }

            return Ok(medicines);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePrescription(int prescriptionID,int medicineID,string? newDosage,string? newDuration)
        {
            if (string.IsNullOrEmpty(newDosage) && string.IsNullOrEmpty(newDuration))
            {
                _logger.LogError("new dosage and new duration can not be null or empty");
                return BadRequest("new dosage and new duration can not be null or empty");
            }

            _logger.LogInformation("Updating the model");
            await _prescriptionService.UpdatePrescription(prescriptionID, medicineID, newDosage, newDuration);
            return Ok("Updated");
        }

        [HttpPost]
        public async Task<IActionResult> AddNewMedicine(PrescriptionMedicineCreationDTO dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid data");
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Adding the new medicine to prescription {id}", dto.PrescriptionID);
            await _prescriptionService.AddNewMedicine(dto);

            return Created();
        }

        [HttpDelete("medicine")]
        public async Task<IActionResult> RemoveMedicine(int medicineID,int prescriptionID)
        {
            _logger.LogInformation("Removing the medicine with id {mid} from the prescription with id {[id}", medicineID, prescriptionID);
            await _prescriptionService.RemoveMedicine(medicineID, prescriptionID);
            return Ok();
        }
    }
}
