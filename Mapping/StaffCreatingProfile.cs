﻿using AutoMapper;

namespace Hospital_Management_System.Mapping
{
    public class StaffCreatingProfile : Profile
    {
        public StaffCreatingProfile()
        {
            CreateMap<Stuff, StuffCreateDto>()
                .ReverseMap();
        }
    }
}
