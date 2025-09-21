using Hospital_Management_System.Models;
using Hospital_Management_System.Repository;
using Hospital_Management_System.UnitOfWork;
using System.Threading.Tasks;

namespace Hospital_Management_System.Services
{
    public class AppointmentServices : IAppointmentServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<AppointmentServices> _logger;

        public AppointmentServices(IMapper mapper,
            ILogger<AppointmentServices> logger,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task AddAppointment(AppointmentCreationDto appointment)
        {
            var appointmentEntity = _mapper.Map<Appointment>(appointment);
            await _unitOfWork.Appointments.AddAppointment(appointmentEntity);
            await _unitOfWork.Complete();
            _logger.LogInformation("Appointment added successfully");
        }

        public async Task DeleteAppointment(int id)
        {
            var appointment = await _unitOfWork.Appointments.GetAppointmentById(id);
            if (appointment == null)
            {
                _logger.LogWarning($"Appointment with ID {id} not found for deletion.");
                return;
            }
            _unitOfWork.Appointments.DeleteAppointment(appointment);
            await _unitOfWork.Complete();
            _logger.LogInformation($"Appointment with ID {id} deleted successfully.");
        }

        public async Task<IEnumerable<AppointmentDisplayDto>> GetAllAppointments()
        {
            var appointments = await _unitOfWork.Appointments.GetAllAppointments();
            if (appointments == null)
            {
                _logger.LogInformation("No appointments found.");
                throw new KeyNotFoundException("No appointments found.");
            }
            return appointments;
        }

        public async Task<AppointmentDisplayDto> GetAppointmentById(int id)
        {
            var appointment = await _unitOfWork.Appointments.GetAppointmentById(id);
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

            var appointments = await _unitOfWork.Appointments.GetAppointmentsByDate(date);
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
            var appointments = await _unitOfWork.Appointments.GetAppointmentsByDoctorId(doctorId);
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
            var appointments = await _unitOfWork.Appointments.GetAppointmentsByPatientId(patientId);
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
            var existingAppointment = await _unitOfWork.Appointments.GetAppointmentById(id);
            if (existingAppointment == null)
            {
                _logger.LogWarning($"Appointment with ID {id} not found for update.");
                throw new KeyNotFoundException($"Appointment with ID {id} not found.");
            }
            existingAppointment.Date = appointment.Date;
            existingAppointment.Time = appointment.Time;
            await _unitOfWork.Appointments.UpdateAppointment(existingAppointment);
            _logger.LogInformation($"Appointment with ID {id} updated successfully.");
        }
    }
}
