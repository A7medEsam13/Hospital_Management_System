
namespace Hospital_Management_System.Services
{
    public class DoctorServices : IDoctorServices
    {
        private readonly ApplicationDbContext _context;

        public DoctorServices(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Add(Doctor doctor)
        {
            await _context.Doctors.AddAsync(doctor);
        }


        public async Task<IEnumerable<Doctor>> GetAll()
        {
            var doctors = await _context.Doctors.ToListAsync();
            return doctors;
        }

        public Doctor GetById(int id)
        {
            var doctor = _context.Doctors.FirstOrDefault(x => x.Id == id);
            if (doctor != null)
            {
                return doctor;
            }
            throw new Exception("Doctor not found!");
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(Doctor doctor)
        {
            var existingDoctor = _context.Doctors.FirstOrDefault(x => x.Id == doctor.Id);
            if (existingDoctor != null)
            {
                _context.Doctors.Update(existingDoctor);
            }
            else
            {
                throw new Exception("Doctor not found!");
            }
        }
    }
}
