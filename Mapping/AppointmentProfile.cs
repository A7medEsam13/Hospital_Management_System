using AutoMapper;

namespace Hospital_Management_System.Mapping
{
    public class AppointmentProfile : Profile
    {
        public AppointmentProfile()
        {
            // Mapping configuration for Appointment entity
            CreateMap<Appointment, AppointmentCreationDto>()
                .ReverseMap(); // Allows mapping in both directions
            // Additional mappings can be added here as needed

            CreateMap<Appointment, AppointmentDisplayDto>()
                .ReverseMap(); // Allows mapping in both directions
            CreateMap<Appointment,AppointmentUpdateDto>()
                .ReverseMap(); // Allows mapping in both directions
        }
    }
}
