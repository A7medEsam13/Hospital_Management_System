using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace Hospital_Management_System.Mapping
{
    public class LaboratoryScreeningProfile : Profile
    {
        public LaboratoryScreeningProfile()
        {
            CreateMap<LaboratoryScreening, LaboratoryScreeningCreationDto>()
                .ReverseMap();

            CreateMap<LaboratoryScreening, LaboratoryScreeningDisplayDto>()
                .ReverseMap();

            CreateMap<LaboratoryScreening, LaboratoryScreeningUpdateDto>()
                .ReverseMap();
        }
    }
}
