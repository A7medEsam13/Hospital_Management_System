using AutoMapper;

namespace Hospital_Management_System.Mapping
{
    public class PatientProfile : Profile
    {
        public PatientProfile() 
        {
            CreateMap<Models.Patient, Dto.PatientCreationDto>()
                .ReverseMap();
        }
    }
}
