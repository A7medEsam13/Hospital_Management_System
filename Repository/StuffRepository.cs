
namespace Hospital_Management_System.Repository
{
    public class StuffRepository : IStuffRepository
    {
        private readonly ApplicationDbContext _context;

        public StuffRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(Stuff staff)
        {
            await _context.Staffs.AddAsync(staff);
        }

        public async Task<IEnumerable<Stuff>> GetAll()
        {
            var staffs = await _context.Staffs
                .AsNoTracking()
                .Where(s=>!s.IsTerminated)
                .ToListAsync();
            return staffs;
        }

        public async Task<Stuff> GetById(string ssn)
        {
            var staff = await _context.Staffs.FirstOrDefaultAsync(s => s.SSN == ssn);
            return staff;
        }

        public async Task<Stuff> GetStuffByUserID(string userID)
        {
            return await _context.Staffs.FirstOrDefaultAsync(s => s.UserId == userID);
        }

        public async Task<string> GetStuffSSN(string userID)
        {
            var stuff = await _context.Staffs.FirstOrDefaultAsync(s => s.UserId == userID);
            return stuff.SSN;
        }

        public async Task<bool> IsExists(string ssn)
        {
            return await _context.Staffs.AnyAsync(s => s.SSN == ssn);
        }

        public async Task Remove(string ssn)
        {
            var staff = await _context.Staffs.FirstOrDefaultAsync(s => s.SSN == ssn);
            staff.IsTerminated = true;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(Stuff staff)
        {
            _context.Staffs.Update(staff);
        }

    }
}
