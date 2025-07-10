using AutoMapper;

namespace Hospital_Management_System.Mapping
{
    public class AppointmentProfile : Profile
    {
        public AppointmentProfile()
        {
            // Mapping configuration for Appointment entity
            CreateMap<Appointment, AppointmentDto>()
                .ReverseMap(); // Allows mapping in both directions
            // Additional mappings can be added here as needed
        }
    }
}
