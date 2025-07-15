using AutoMapper;

namespace Hospital_Management_System.Mapping
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleDto, Role>()
                .ReverseMap();

        }
    }
}
