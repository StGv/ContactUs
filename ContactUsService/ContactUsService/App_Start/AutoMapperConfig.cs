using AutoMapper;
using System;
using System.Linq;

namespace ContactUsService
{
    public static class AutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<Models.CustomerMessage, Controllers.DTOs.ContactUsFormDTO>()
                    .ForMember(dest => dest.fullName, opt => opt.MapFrom(src => string.Join(" ", src.Customer.FirstName, src.Customer.LastName)))
                    .ForMember(dest => dest.message, opt => opt.MapFrom(src => src.Text))
                    .ForMember(dest => dest.email, opt => opt.MapFrom(src => src.Customer.Email));

            });
        } 
    }
}