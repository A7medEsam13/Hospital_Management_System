using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Receptionest")]
    public class BillsController(ILogger<BillsController> logger,
        IBillServices billServices) : ControllerBase
    {
        private readonly ILogger<BillsController> _logger = logger;
        private readonly IBillServices _billServices = billServices;

        [HttpPost]
        public async Task<IActionResult> CreateNewBill(BillCreationDto dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid Model");
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Creating the new Bill");
            await _billServices.CreateNewBill(dto);
            var bill = await _billServices.GetAllPatientBills(dto.PatientID);
            return Ok(bill);
        }

        [HttpPut("pay")]
        public async Task<IActionResult> PayBill(int billID)
        {
            var paid = await _billServices.PayBill(billID);

            if (paid)
            {
                return Ok("Bill Paid Successfully");
            }
            return BadRequest();

        }

        [HttpGet("patientbills")]
        public async Task<IActionResult> GetAllPatientBills(int patientID)
        {
            _logger.LogInformation("Getting all bills of patient with ID {id}", patientID);
            var bills = await _billServices.GetAllPatientBills(patientID);

            return Ok(bills);
        }

        

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByID(int id)
        {
            _logger.LogInformation("Getting the Bill with ID {id}", id);
            var bill = await _billServices.GetByID(id);

            return Ok(bill);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _billServices.Delete(id);

            return Ok("Deleted");
        }
    }
}
