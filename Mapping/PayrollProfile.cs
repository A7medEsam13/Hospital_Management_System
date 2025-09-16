namespace Hospital_Management_System.Mapping
{
    public class PayrollProfile : Profile
    {
        public PayrollProfile()
        {
            CreateMap<Payroll, PayrollCreateDto>().ReverseMap();

            CreateMap<Payroll, PayrollUpdateDto>().ReverseMap();

            CreateMap<Payroll, PayrollDisplayDto>().ReverseMap();
        }
    }
}
