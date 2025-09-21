namespace Hospital_Management_System.Mapping
{
    public class BillProfile : Profile
    {
        public BillProfile()
        {
            CreateMap<Bill, BillCreationDto>()
                .ReverseMap();

            CreateMap<Bill, BillDisplayDto>()
                .ForMember(dest => dest.MedicineCost, options => options.MapFrom(src => src.MedicineCost))
                .ForMember(dest => dest.TestsCost, options => options.MapFrom(src => src.TestCost))
                .ForMember(dest => dest.RoomCost, options => options.MapFrom(src => src.RoomCost))
                .ReverseMap();
        }
    }
}
