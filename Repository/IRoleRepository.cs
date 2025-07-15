namespace Hospital_Management_System.Repository
{
    public interface IRoleRepository
    {
        public Task AddRoleAsync(Role role);
        public Task<Role> GetRoleByNameAsync(string roleId);
        public Task<IEnumerable<Role>> GetAllRolesAsync();
        public Task SaveAsync();
    }
}
