using AutoMapper;

namespace Hospital_Management_System.Mapping
{
    public class DoctorUpdateProfile : Profile
    {
        public DoctorUpdateProfile()
        {
            CreateMap<Doctor, DoctorUpdateDto>()
                .ReverseMap();
        }
    }
}
