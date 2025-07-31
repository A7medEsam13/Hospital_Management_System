using Hospital_Management_System.Models;

namespace Hospital_Management_System.Services
{
    public interface IAppointmentServices
    {
        public Task AddAppointment(AppointmentCreationDto appointment); //
        public Task<IEnumerable<AppointmentDisplayDto>> GetAllAppointments(); //
        public Task<AppointmentDisplayDto> GetAppointmentById(int id); //
        public Task UpdateAppointment(int id, AppointmentUpdateDto appointment); //
        public Task DeleteAppointment(int id); //
        public Task<IEnumerable<AppointmentDisplayDto>> GetAppointmentsByPatientId(int patientId); //
        public Task<IEnumerable<AppointmentDisplayDto>> GetAppointmentsByDoctorId(string doctorId); //
        public Task<IEnumerable<AppointmentDisplayDto>> GetAppointmentsByDate(DateOnly date); //
    }
}
