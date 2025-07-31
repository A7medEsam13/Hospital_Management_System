using Hospital_Management_System.Models;
using Hospital_Management_System.Repository;
using System.Threading.Tasks;

namespace Hospital_Management_System.Services
{
    public class AppointmentServices : IAppointmentServices
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AppointmentServices> _logger;

        public AppointmentServices(IAppointmentRepository appointmentRepository,
            IMapper mapper,
            ILogger<AppointmentServices> logger)
        {
            _appointmentRepository = appointmentRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task AddAppointment(AppointmentCreationDto appointment)
        {
            var appointmentEntity = _mapper.Map<Appointment>(appointment);
            await _appointmentRepository.AddAppointment(appointmentEntity);
            await _appointmentRepository.SaveChangesAsync();
            _logger.LogInformation("Appointment added successfully");
        }

        public async Task DeleteAppointment(int id)
        {
            var appointment = await _appointmentRepository.GetAppointmentById(id);
            if (appointment == null)
            {
                _logger.LogWarning($"Appointment with ID {id} not found for deletion.");
                return;
            }
            _appointmentRepository.DeleteAppointment(appointment);
            await _appointmentRepository.SaveChangesAsync();
            _logger.LogInformation($"Appointment with ID {id} deleted successfully.");
        }

        public async Task<IEnumerable<AppointmentDisplayDto>> GetAllAppointments()
        {
            var appointments = await _appointmentRepository.GetAllAppointments();
            if (appointments == null)
            {
                _logger.LogInformation("No appointments found.");
                throw new KeyNotFoundException("No appointments found.");
            }
            return appointments;
        }

        public async Task<AppointmentDisplayDto> GetAppointmentById(int id)
        {
            var appointment = await _appointmentRepository.GetAppointmentById(id);
            if (appointment == null)
            {
                _logger.LogWarning($"Appointment with ID {id} not found.");
                throw new KeyNotFoundException($"Appointment with ID {id} not found.");
            }
            var dto = _mapper.Map<AppointmentDisplayDto>(appointment);
            _logger.LogInformation($"Appointment with ID {id} retrieved successfully.");
            return dto;
        }

        public async Task<IEnumerable<AppointmentDisplayDto>> GetAppointmentsByDate(DateOnly date)
        {

            var appointments = await _appointmentRepository.GetAppointmentsByDate(date);
            if(appointments == null || !appointments.Any())
            {
                _logger.LogInformation($"No appointments found for date {date}.");
                throw new KeyNotFoundException($"No appointments found for date {date}.");
            }
            _logger.LogInformation($"Appointments for date {date} retrieved successfully.");
            return appointments;
        }

        public async Task<IEnumerable<AppointmentDisplayDto>> GetAppointmentsByDoctorId(string doctorId)
        {
            var appointments = await _appointmentRepository.GetAppointmentsByDoctorId(doctorId);
            if (appointments == null || !appointments.Any())
            {
                _logger.LogInformation($"No appointments found for doctor ID {doctorId}.");
                throw new KeyNotFoundException($"No appointments found for doctor ID {doctorId}.");
            }
            _logger.LogInformation($"Appointments for doctor ID {doctorId} retrieved successfully.");
            return appointments;
        }

        public async Task<IEnumerable<AppointmentDisplayDto>> GetAppointmentsByPatientId(int patientId)
        {
            var appointments = await _appointmentRepository.GetAppointmentsByPatientId(patientId);
            if (appointments == null || !appointments.Any())
            {
                _logger.LogInformation($"No appointments found for patient ID {patientId}.");
                throw new KeyNotFoundException($"No appointments found for patient ID {patientId}.");
            }
            _logger.LogInformation($"Appointments for patient ID {patientId} retrieved successfully.");
            return appointments;
        }

        public async Task UpdateAppointment(int id, AppointmentUpdateDto appointment)
        {
            var existingAppointment = await _appointmentRepository.GetAppointmentById(id);
            if (existingAppointment == null)
            {
                _logger.LogWarning($"Appointment with ID {id} not found for update.");
                throw new KeyNotFoundException($"Appointment with ID {id} not found.");
            }
            var updatedAppointment = _mapper.Map<Appointment>(appointment);
            _appointmentRepository.UpdateAppointment(updatedAppointment);
            _appointmentRepository.SaveChangesAsync().Wait();
            _logger.LogInformation($"Appointment with ID {id} updated successfully.");
        }
    }
}
