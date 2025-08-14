namespace Hospital_Management_System.Mapping
{
    public class DiagnosisProfile : Profile
    {
        public DiagnosisProfile()
        {
            CreateMap<Diagnosis, DiagnosisCreationDto>()
                .ReverseMap();

            CreateMap<Diagnosis,DiagnosisUpdateDto>()
                .ReverseMap();

        }
    }
}
