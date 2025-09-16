using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management_System.Controllers
{
    [Authorize("Admin")]
    [Route("api/[controller]")]
    [ApiController]
    
    public class PayrollsController(ILogger<PayrollsController> _logger,
        IPayrollServices _payrollServices) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateNewPayroll(PayrollCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model");
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Creating the payroll");
            await _payrollServices.CreatePayroll(dto);
            return Created();
        }

        [HttpPut]
        public async Task<IActionResult> Update(string ssn, PayrollUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid Model");
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Updating the payroll");
            await _payrollServices.UpdatePayroll(ssn, dto);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var payroll = await _payrollServices.GetByID(id);
            if (payroll is null)
            {
                _logger.LogError("there is no payroll with ID {id} exists in the database", id);
                return NotFound(id);
            }

            _logger.LogInformation("Deleting the payroll {id}", id);
            await _payrollServices.Delete(id);
            _logger.LogInformation("the payroll deleted successfully");

            return Ok("Deleted");
        }

        [HttpGet("drawdates")]
        public async Task<IActionResult> GetDrawDates(string ssn)
        {
            if (ssn.Length > 16 || ssn.Length < 16)
            {
                _logger.LogError("Invalid SSN");
                return BadRequest("Invalid SSN");
            }

            _logger.LogInformation("Getting draw dates of the stuff {ssn} payroll", ssn);
            var dates = await _payrollServices.GetAllDrawDates(ssn);
            return Ok(dates);
        }


        [HttpGet("updateddates")]
        public async Task<IActionResult> GetUpdatedDates(string ssn)
        {
            if (ssn.Length > 16 || ssn.Length < 16)
            {
                _logger.LogError("Invalid SSN");
                return BadRequest("Invalid SSN");
            }

            _logger.LogInformation("Getting updated dates of the stuff {ssn} payroll", ssn);
            var dates = await _payrollServices.GetAllUpdatedDates(ssn);

            return Ok(dates);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetPayrollByID(int id)
        {
            _logger.LogInformation("getting the payroll {id} fromthe DB", id);
            var payroll = await _payrollServices.GetByID(id);

            return Ok(payroll);
        }

        [HttpGet]
        public async Task<IActionResult> GetPayrollBySSN(string ssn)
        {
            _logger.LogInformation("Getting the stuff {ssn} payroll", ssn);
            var payroll = await _payrollServices.GetBySSN(ssn);

            return Ok(payroll);
        }

    }


}
