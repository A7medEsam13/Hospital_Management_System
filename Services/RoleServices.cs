
using AutoMapper;
using Hospital_Management_System.Repository;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Hospital_Management_System.Services
{
    public class RoleServices : IRoleServices
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RoleServices(IRoleRepository roleRepository, 
            IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<bool> AddRoleAsync(RoleDto roleDto)
        {
            var exisitngRole = await _roleRepository.GetRoleByNameAsync(roleDto.Name);
            if (exisitngRole != null)
            {
                return false;
            }
            else
            {
                if (roleDto.Name == "")
                {
                    throw new ArgumentException("Role name cannot be empty.");
                }
                var role = _mapper.Map<Role>(roleDto);
                await _roleRepository.AddRoleAsync(role);
                await _roleRepository.SaveAsync();
                return true;
            }
        }

        public Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            return _roleRepository.GetAllRolesAsync();
        }

        public async Task<Role> GetRoleByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Role name cannot be null or empty.", nameof(name));
            }
            return await _roleRepository.GetRoleByNameAsync(name);
        }
    }
}
