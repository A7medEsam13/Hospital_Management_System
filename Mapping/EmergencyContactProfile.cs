namespace Hospital_Management_System.Mapping
{
    public class EmergencyContactProfile : Profile
    {
        public EmergencyContactProfile()
        {
            CreateMap<EmergencyContact,EmergencyContactCreationDto>()
                .ReverseMap();

            CreateMap<EmergencyContact, EmergencyContactDisplayDto>()
                .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.Patient.FirstName + " " + src.Patient.LastName))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();
        }
    }
}
