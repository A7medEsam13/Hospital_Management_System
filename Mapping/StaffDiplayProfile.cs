using AutoMapper;

namespace Hospital_Management_System.Mapping
{
    public class StaffDiplayProfile : Profile
    {
        public StaffDiplayProfile()
        {
            CreateMap<Staff, StaffDisplayDto>()     // mapping from staff to dto.
                .ReverseMap();
        }
    }
}
