﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffsController : ControllerBase
    {
        private readonly ILogger<StaffsController> _logger;
        private readonly IMapper _mapper;
        private readonly IStaffServices _staffServices;

        public StaffsController(ILogger<StaffsController> logger,
            IMapper mapper,
            IStaffServices staffServices)
        {
            _logger = logger;
            _mapper = mapper;
            _staffServices = staffServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStaffs()
        {
            var staffs = await _staffServices.GetAll();
            var staffDtos = _mapper.Map<IEnumerable<StuffDisplayDto>>(staffs);
            _logger.LogInformation("Retrieved all staff members successfully.");
            return Ok(staffDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStaffById(string id)
        {
            var staff = await _staffServices.GetById(id);
            if (staff == null)
            {
                _logger.LogError($"Staff with SSN {id} not found.");
                return NotFound($"Staff with SSN {id} not found.");
            }
            var staffDto = _mapper.Map<StuffDisplayDto>(staff);
            _logger.LogInformation($"Retrieved staff member with SSN {id} successfully.");
            return Ok(staffDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddStaff( StuffCreateDto dto)
        {
            if (dto == null)
            {
                _logger.LogError("Received null staff object.");
                return BadRequest("Staff object cannot be null.");
            }
            await _staffServices.Create(dto);
            _logger.LogInformation($"Added new staff member with SSN {dto.SSN} successfully.");
            return Created();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStaff([FromBody] StuffUpdateDto dto)
        {
            if (dto == null)
            {
                _logger.LogError("Received null staff object for update.");
                return BadRequest("Staff object cannot be null.");
            }
            var existingStaff = await _staffServices.GetById(dto.SSN);
            if (existingStaff == null)
            {
                _logger.LogError($"Staff with SSN {dto.SSN} not found for update.");
                return NotFound($"Staff with SSN {dto.SSN} not found.");
            }
            await _staffServices.Update(dto);
            _logger.LogInformation($"Updated staff member with SSN {dto.SSN} successfully.");
            return Ok();
        }

        [HttpDelete("{id:alpha}")]
        public async Task<IActionResult> TerminateStaff(string id)
        {
            var staff = await _staffServices.GetById(id);
            if (staff == null)
            {
                _logger.LogError($"Staff with SSN {id} not found for deletion.");
                return NotFound($"Staff with SSN {id} not found.");
            }
            await _staffServices.Terminate(id);
            _logger.LogInformation($"Deleted staff member with SSN {id} successfully.");
            return Ok();
        }
    }
}
