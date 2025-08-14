namespace Hospital_Management_System.Mapping
{
    public class DoctorProfile : Profile
    {
        public DoctorProfile()
        {
            CreateMap<DoctorCreateDto, Doctor>()
                .ReverseMap();

            CreateMap<DoctorUpdateDto, Doctor>()
                .ReverseMap();
            CreateMap<DoctorDisplayDto, Doctor>()
                .ReverseMap();
        }
    }
}
