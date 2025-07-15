
namespace Hospital_Management_System.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddRoleAsync(Role role)
        {
            await _context.Roles.AddAsync(role);
        }

        public async Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            var roles = await _context.Roles.ToListAsync();
            return roles;
        }

        public async Task<Role> GetRoleByNameAsync(string name)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(_context => _context.Name == name);
            return role;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
