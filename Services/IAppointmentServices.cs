using Hospital_Management_System.Models;

namespace Hospital_Management_System.Services
{
    public interface IAppointmentServices
    {
        public Task AddAppointment(Appointment appointment); //
        public Task<List<Appointment>> GetAllAppointments(); //
        public Task<Appointment> GetAppointmentById(int id); //
        public void UpdateAppointment(int id, Appointment appointment); //
        public Task DeleteAppointment(int id); //
        public Task<IEnumerable<Appointment>> GetAppointmentsByPatientId(int patientId); //
        public Task<List<Appointment>> GetAppointmentsByDoctorId(string doctorId); //
        public Task<List<Appointment>> GetAppointmentsByDate(DateOnly date); //
        public Task SaveChangesAsync();
    }
}
