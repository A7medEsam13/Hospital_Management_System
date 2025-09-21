namespace Hospital_Management_System.Repository
{
    public interface IAppointmentRepository
    {
        public Task AddAppointment(Appointment appointment); //
        public Task<IEnumerable<AppointmentDisplayDto>> GetAllAppointments(); //
        public Task<Appointment> GetAppointmentById(int id); //
        public Task UpdateAppointment(Appointment appointment); //
        public void DeleteAppointment(Appointment appointment); //
        public Task<IEnumerable<AppointmentDisplayDto>> GetAppointmentsByPatientId(int patientId); //
        public Task<IEnumerable<AppointmentDisplayDto>> GetAppointmentsByDoctorId(string doctorId); //
        public Task<IEnumerable<AppointmentDisplayDto>> GetAppointmentsByDate(DateOnly date); //
        public Task SaveChangesAsync();
    }
}
