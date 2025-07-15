using Hospital_Management_System.Repository;

namespace Hospital_Management_System.Services
{
    public interface IRoleServices
    {
        public Task<bool> AddRoleAsync(RoleDto roleDto);
        public Task<Role> GetRoleByNameAsync(string name);
        public Task<IEnumerable<Role>> GetAllRolesAsync();
    }
}
