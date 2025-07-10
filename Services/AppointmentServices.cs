using Hospital_Management_System.Models;

namespace Hospital_Management_System.Services
{
    public class AppointmentServices : IAppointmentServices
    {
        private readonly ApplicationDbContext _context;
        public AppointmentServices(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAppointment(Appointment appointment)
        {
            await _context.Appointments.AddAsync(appointment);
        }

        public  Task DeleteAppointment(int id)
        {
            var appointment = _context.Appointments.Find(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
                return Task.CompletedTask;
            }
            throw new KeyNotFoundException("Appointment not found");
        }

        public async Task<List<Appointment>> GetAllAppointments()
        {
            var appointments = await _context.Appointments.ToListAsync();
            return appointments;
        }

        public Task<Appointment> GetAppointmentById(int id)
        {
            var appointment = _context.Appointments.Find(id);
            if (appointment != null)
            {
                return Task.FromResult(appointment);
            }
            throw new KeyNotFoundException("Appointment not found");
        }

        public async Task<List<Appointment>> GetAppointmentsByDate(DateOnly date)
        {
            var appointments = await _context.Appointments
                .Where(a => a.Date == date)
                .ToListAsync();
            return appointments;
        }

        public async Task<List<Appointment>> GetAppointmentsByDoctorId(int doctorId)
        {
            var appointments = await _context.Appointments
                .Where(a => a.DoctorId == doctorId)
                .ToListAsync();
            return appointments;
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByPatientId(int patientId)
        {
            var appointments = await _context.Appointments
                .Where(a => a.PatientId == patientId)
                .ToListAsync();
            return appointments;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void UpdateAppointment(Appointment appointment)
        {
            _context.Appointments.Update(appointment);
        }

        public void UpdateAppointment(int id, Appointment appointment)
        {
            throw new NotImplementedException();
        }
    }
}
