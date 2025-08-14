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


        public async Task<IEnumerable<DoctorDisplayDto>> GetAll()
        {
            var doctors = await _context.Doctors
                .Select(d=> new DoctorDisplayDto
                {
                    SSN = d.SSN,
                    FirstName = d.FirstName,
                    LastName = d.LastName,
                    JoinDate = d.JoinDate,
                    Email = d.Email,
                    Address = d.Address,
                    DepartmentName = d.DepartmentName,
                    Salary = d.Salary,
                    Qualification = d.Qualification,
                    Specialization = d.Specialization
                })
                .ToListAsync();
            return doctors;
        }

        public async Task<Doctor> GetById(string id)
        {
            var doctor = await _context.Doctors.FirstOrDefaultAsync(x => x.SSN == id);

            return doctor;
            
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(DoctorUpdateDto doctor)
        {
            var existingDoctor = _context.Doctors.FirstOrDefault(x => x.SSN == doctor.SSN);
            _context.Doctors.Update(existingDoctor);
        }
    }
}
