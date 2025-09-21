using System.Threading.Tasks;

namespace Hospital_Management_System.Repository
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly ApplicationDbContext _context;

        public DoctorRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Add(Doctor doctor)
        {
            await _context.Doctors.AddAsync(doctor);
        }


        public async Task<IEnumerable<Doctor>> GetAll()
        {
            var doctors = await _context.Doctors
                .ToListAsync();
            return doctors;
        }

        public async Task<Doctor> GetById(string id)
        {
            var doctor = await _context.Doctors.FirstOrDefaultAsync(x => x.SSN == id);

            return doctor;
            
        }

       


        public async Task Update(Doctor doctor)
        {
            await _context.Doctors
                .Where(d => d.SSN == doctor.SSN)
                .ExecuteUpdateAsync(d => d
                .SetProperty(o => o.Qualification, n => doctor.Qualification)
                .SetProperty(o => o.Specialization, n => doctor.Specialization));

            await _context.Staffs
                .Where(d => d.SSN == doctor.SSN)
                .ExecuteUpdateAsync(s => s.
                SetProperty(o => o.Address, n => doctor.Address)
                .SetProperty(o => o.DepartmentName, n => doctor.DepartmentName));
        }
    }
}
