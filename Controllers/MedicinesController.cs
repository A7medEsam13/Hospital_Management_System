using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;

namespace Hospital_Management_System.Controllers
{
    [Authorize("Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class MedicinesController(IMedicineServices _medicineServices, 
        ILogger<MedicinesController> _logger) : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllMedicines()
        {
            _logger.LogInformation("Getting all medicines");
            var medicines = _medicineServices.GetAllMedicines();

            return Ok(medicines);
        }

        [HttpGet("{name:alpha}")]
        public async Task<IActionResult> GetMedicineByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                _logger.LogError("Invalid Name");
                return BadRequest("Invalid Name");
            }

            _logger.LogInformation("Getting the entity with name {name}", name);
            var medicine = await _medicineServices.GetMedicineByName(name);

            return Ok(medicine);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetMedicineByID(int id)
        {
            _logger.LogInformation("Getting th medicibne with ID {id}", id);
            var medicine = await _medicineServices.GetMedicineByID(id);

            return Ok(medicine);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Deleting the medicine with ID {id}", id);
            await _medicineServices.DeleteMedicine(id);

            return Ok();
        }

        [HttpPut("cost")]
        public async Task<IActionResult> UpdateMedicineCost(int id,decimal newCost)
        {
            _logger.LogInformation("Updating the cost of medicine {id}", id);
            await _medicineServices.UpdateMedicineCost(id, newCost);

            return Ok();
     
        }

        [HttpPut("Quantity")]
        public async Task<IActionResult> UpdateMedicineQuantity(int id,int newQuantity)
        {
            _logger.LogInformation("Updating the quantity of medicine {id}", id);
            await _medicineServices.UpdateMedicineQuantity(id, newQuantity);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewMedicine(MedicineCreationDto dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid Model");
                return BadRequest(ModelState); 
            }

            _logger.LogInformation("Adding the new medicine");
            await _medicineServices.AddNewMedicine(dto);
            return Created();
        }
    }
}
