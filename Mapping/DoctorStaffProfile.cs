using AutoMapper;

namespace Hospital_Management_System.Mapping
{
    public class DoctorStaffProfile : Profile
    {
        public DoctorStaffProfile()
        {
            // mapping from dto to doctor.
            CreateMap<DoctorCreateDto, Doctor>()  // <source, destination>
                .ForMember(dest => dest.Qualification, opt => opt.MapFrom(src => src.Qualification))
                .ForMember(dest => dest.Specialization, opt => opt.MapFrom(src => src.Specialization))
                .ReverseMap();

            // mapping from dto to staff.
            CreateMap<DoctorCreateDto, Stuff>()  // <source, destination>
            .ForMember(dest=>dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.JoinDate, opt => opt.MapFrom(src => src.JoinDate))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.DepartmentName))
                .ForMember(dest => dest.SSN, opt => opt.MapFrom(src => src.SSN))
                .ReverseMap();
        }
    }
}
