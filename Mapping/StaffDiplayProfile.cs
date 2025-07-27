using AutoMapper;

namespace Hospital_Management_System.Mapping
{
    public class StaffDiplayProfile : Profile
    {
        public StaffDiplayProfile()
        {
            CreateMap<Stuff, StuffDisplayDto>()     // mapping from staff to dto.
                .ReverseMap();
        }
    }
}
