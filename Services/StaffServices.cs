
namespace Hospital_Management_System.Services
{
    public class StaffServices : IStaffServices
    {
        private readonly ApplicationDbContext _context;

        public StaffServices(ApplicationDbContext context)
        {
            _context = context; 
        }
        public async Task Add(Staff staff)
        {
            await _context.Staffs.AddAsync(staff);
        }

        public async Task<IEnumerable<Staff>> GetAll()
        {
            var staffs = await _context.Staffs.ToListAsync();
            return staffs;
        }

        public async Task<Staff> GetById(string ssn)
        {
            var staff = await _context.Staffs.FirstOrDefaultAsync(s => s.SSN == ssn);
            return staff;
        }

        public async Task<Staff> GetById(int id)
        {
            var staff = await _context.Staffs.FirstOrDefaultAsync(s => s.SSN == id.ToString());
            if(staff == null)
            {
                throw new Exception("Staff not found");
            }
            return staff;
        }

        public async Task Remove(string ssn)
        {
            var staff = await _context.Staffs.FirstOrDefaultAsync(s => s.SSN == ssn);
            if(staff == null)
            {
                throw new NullReferenceException($"Staff with SSN {ssn} not exist");
            }

            staff.IsTerminated = true;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(Staff staff)
        {
            var existingStaff = _context.Staffs.FirstOrDefault(s => s.SSN == staff.SSN);
            if (existingStaff != null)
            {
                _context.Staffs.Update(staff);
            }
            else
            {
                throw new Exception("Staff not found");
            }
        }

        
    }
}
