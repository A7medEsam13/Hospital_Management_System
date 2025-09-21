using AutoMapper;
using Hospital_Management_System.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Hospital_Management_System.Controllers
{
    [Authorize(Roles ="Receptionest,Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly ILogger<AppointmentsController> _logger;
        private readonly IAppointmentServices _appointmentServices;
        private readonly IMapper _mapper;
        private readonly IPatientServices _patientServices;
        private readonly IDoctorServices _doctorServices;


        // Injecting the services through the constructor.
        public AppointmentsController(
            ILogger<AppointmentsController> logger,
            IAppointmentServices appointmentServices,
            IMapper mapper,
            IPatientServices patientServices,
            IDoctorServices doctorServices
            )
        {
            _logger = logger;
            _appointmentServices = appointmentServices;
            _mapper = mapper;
            _patientServices = patientServices;
            _doctorServices = doctorServices;
        }


        // Booking a new appointment
        [HttpPost]
        public async Task<IActionResult> BookAppointment(AppointmentCreationDto appointmentDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Model state is invalid for booking an appointment.");
                return BadRequest(ModelState);
            }
            // Logic to book an appointment
            if (appointmentDto == null)
            {
                // Log the error if appointment data is null
                _logger.LogError("Appointment data is null.");
                return BadRequest("Invalid appointment data.");
            }
            
            
            // Here you would typically save the appointment to a database
            await _appointmentServices.AddAppointment(appointmentDto);
            
            // Log the successful booking
            _logger.LogInformation("Appointment booked successfully for patient ID {PatientId} with doctor ID {DoctorId}.", appointmentDto.PatientId, appointmentDto.DoctorId);
            
            // For now, we will just return a success message
            return Ok("Appointment booked successfully.");
        }


        // getting all appointments
        [HttpGet("All-Appointments")]
        public async Task<IActionResult> GetAllAppointments()
        {
            // Logic to get all appointments
            var appointments = await _appointmentServices.GetAllAppointments();
            if (appointments == null || !appointments.Any())
            {
                // Log the case where no appointments are found
                _logger.LogError("No appointments found.");
                return NotFound("No appointments found.");
            }
            return Ok(appointments);
        }

        // getting appointment by id
        [HttpGet("id/{id:int}")]
        public async Task<IActionResult> GetAppointmentById(int id)
        {
            // Logic to get an appointment by id
            var appointment = await _appointmentServices.GetAppointmentById(id);
            if (appointment == null)
            {
                // Log the case where the appointment is not found
                _logger.LogError("Appointment with ID {Id} not found.", id);
                return NotFound($"Appointment with ID {id} not found.");
            }
            var appointmentDto = _mapper.Map<AppointmentDisplayDto>(appointment);
            return Ok(appointmentDto);
        }

        // get appointments by date
        [HttpGet]
        public async Task<IActionResult> GetAppointmentsByDate(DateOnly date)
        {
            // Logic to get appointments by date
            var appointments = await _appointmentServices.GetAppointmentsByDate(date);
            if (appointments == null || !appointments.Any())
            {
                // Log the case where no appointments are found for the given date
                _logger.LogError("No appointments found for the date {Date}.", date);
                return NotFound($"No appointments found for the date {date}.");
            }
            return Ok(appointments);
        }

        // get appointments by patient id
        [HttpGet("{patientId:int}")]
        public async Task<IActionResult> GetAppointmentsByPatientId(int patientId)
        {
            // Logic to get appointments by patient id
            var appointments = await _appointmentServices.GetAppointmentsByPatientId(patientId);
            if (appointments == null || appointments.Count() == 0)
            {
                // Log the case where no appointments are found for the given patient id
                _logger.LogError("No appointments found for patient ID {PatientId}.", patientId);
                return NotFound($"No appointments found for patient ID {patientId}.");
            }
            var appointmentDtos = _mapper.Map<IEnumerable<AppointmentDisplayDto>>(appointments);
            return Ok(appointmentDtos);
        }

        // get appointments by doctor id
        [HttpGet("doctor")]
        public async Task<IActionResult> GetAppointmentsByDoctorId(string doctorId)
        {
            // Logic to get appointments by doctor id
            var appointments = await _appointmentServices.GetAppointmentsByDoctorId(doctorId);
            if (appointments == null || !appointments.Any())
            {
                // Log the case where no appointments are found for the given doctor id
                _logger.LogError("No appointments found for doctor ID {DoctorId}.", doctorId);
                return NotFound($"No appointments found for doctor ID {doctorId}.");
            }
            var appointmentDtos = _mapper.Map<IEnumerable<AppointmentDisplayDto>>(appointments);
            return Ok(appointmentDtos);
        }

        // updating an appointment
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAppointment(int id, AppointmentUpdateDto appointmentDto)
        {
            // Logic to update an appointment
            if (appointmentDto == null || id <= 0)
            {
                // Log the error if appointment data is null or ID is invalid
                _logger.LogError("Invalid appointment data or ID.");
                return BadRequest("Invalid appointment data.");
            }
            var existingAppointment = await _appointmentServices.GetAppointmentById(id);
            if (existingAppointment == null)
            {
                // Log the case where the appointment to update is not found
                _logger.LogError("Appointment with ID {Id} not found for update.", id);
                return NotFound($"Appointment with ID {id} not found.");
            }
            await _appointmentServices.UpdateAppointment(id, appointmentDto);
            // Log the successful update
            _logger.LogInformation("Appointment with ID {Id} updated successfully.", id);
            return Ok("Appointment updated successfully.");
        }

        // deleting an appointment
        [HttpDelete]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            // Logic to delete an appointment
            if (id <= 0)
            {
                // Log the error if the appointment ID is invalid
                _logger.LogError("Invalid appointment ID: {Id}.", id);
                return BadRequest("Invalid appointment ID.");
            }
            var existingAppointment = await _appointmentServices.GetAppointmentById(id);
            if (existingAppointment == null)
            {
                // Log the case where the appointment to delete is not found
                _logger.LogError("Appointment with ID {Id} not found for deletion.", id);
                return NotFound($"Appointment with ID {id} not found.");
            }
            await _appointmentServices.DeleteAppointment(id);
            // Log the successful deletion
            _logger.LogInformation("Appointment with ID {Id} deleted successfully.", id);
            return Ok("Appointment deleted successfully.");
        }
    }
}
