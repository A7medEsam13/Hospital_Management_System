namespace Hospital_Management_System.Mapping
{
    public class MedicineProfile : Profile
    {
        public MedicineProfile()
        {
            CreateMap<Medicine, MedicineCreationDto>()
                .ReverseMap();

            CreateMap<Medicine, MedicineDisplayDTO>()
                .ReverseMap();
        }
    }
}
