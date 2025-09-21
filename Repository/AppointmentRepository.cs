using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Hospital_Management_System.Repository
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly CancellationToken _cancellationToken;
        public AppointmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAppointment(Appointment appointment)
        {
            await _context.Appointments.AddAsync(appointment);
        }

        public void DeleteAppointment(Appointment appointment)
        {
            _context.Appointments.Remove(appointment);
        }

        public async Task<IEnumerable<AppointmentDisplayDto>> GetAllAppointments()
        {
            var appointments = 
                await _context.Appointments
                .Select(a => new AppointmentDisplayDto
                {
                    Id = a.Id,
                    ScheduledOn = a.ScheduledOn,
                    Date = a.Date,
                    Time = a.Time,
                    PatientId = a.PatientId,
                    DoctorId = a.DoctorId,
                    PatientName = a.Patient.FirstName + " " + a.Patient.LastName,
                    DoctorName = a.Doctor.FirstName + " " + a.Doctor.LastName
                })
                .ToListAsync();
            return appointments;
        }

        public async Task<Appointment> GetAppointmentById(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            return appointment;
        }

        public async Task<IEnumerable<AppointmentDisplayDto>> GetAppointmentsByDate(DateOnly date)
        {
            var appointments = await _context.Appointments
                .Where(a => a.Date == date)
                .Select(a => new AppointmentDisplayDto
                {
                    Id = a.Id,
                    ScheduledOn = a.ScheduledOn,
                    Date = a.Date,
                    Time = a.Time,
                    PatientId = a.PatientId,
                    DoctorId = a.DoctorId,
                    PatientName = a.Patient.FirstName + " " + a.Patient.LastName,
                    DoctorName = a.Doctor.FirstName + " " + a.Doctor.LastName
                })
                .ToListAsync();
            return appointments;
        }

        public async Task<IEnumerable<AppointmentDisplayDto>> GetAppointmentsByDoctorId(string doctorId)
        {
            var appointments = await _context.Appointments
                .Where(a => a.DoctorId == doctorId)
                .Select(a => new AppointmentDisplayDto
                {
                    Id = a.Id,
                    ScheduledOn = a.ScheduledOn,
                    Date = a.Date,
                    Time = a.Time,
                    PatientId = a.PatientId,
                    DoctorId = a.DoctorId,
                    PatientName = a.Patient.FirstName + " " + a.Patient.LastName,
                    DoctorName = a.Doctor.FirstName + " " + a.Doctor.LastName
                })
                .ToListAsync();
            return appointments;
        }

        public async Task<IEnumerable<AppointmentDisplayDto>> GetAppointmentsByPatientId(int patientId)
        {
            var appointments = await _context.Appointments
                .Where(a => a.PatientId == patientId)
                .Select(a => new AppointmentDisplayDto
                {
                    Id = a.Id,
                    ScheduledOn = a.ScheduledOn,
                    Date = a.Date,
                    Time = a.Time,
                    PatientId = a.PatientId,
                    DoctorId = a.DoctorId,
                    PatientName = a.Patient.FirstName + " " + a.Patient.LastName,
                    DoctorName = a.Doctor.FirstName + " " + a.Doctor.LastName
                })
                .ToListAsync();
            return appointments;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAppointment(Appointment appointment)
        {
            await _context.Appointments
                .Where(a => a.Id == appointment.Id)
                .ExecuteUpdateAsync(s => s
                .SetProperty(o => o.Date, n => appointment.Date)
                .SetProperty(o => o.Time, n => appointment.Time));
        }

       
    }
}
