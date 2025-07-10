using AutoMapper;

namespace Hospital_Management_System.Mapping
{
    public class StaffCreatingProfile : Profile
    {
        public StaffCreatingProfile()
        {
            CreateMap<Staff, StaffCreatingDto>()
                .ReverseMap();
        }
    }
}
