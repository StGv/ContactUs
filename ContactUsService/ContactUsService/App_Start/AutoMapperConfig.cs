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

                cfg.CreateMap<Controllers.DTOs.ContactUsFormDTO, Models.CustomerMessage>()
                     .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => new Models.Customer() { FirstName = getFirstName(src.fullName) , LastName = getLastName(src.fullName), Email = src.email }))
                     .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.message))
                     .ForMember(dest => dest.ReceivedOn, opt => opt.Ignore());
            });
        } 

        public static string getFirstName(string fullname)
        {
            return fullname.Trim().Split(' ')[0];
        }
        public static string getLastName(string fullname)
        {
            return string.Join(" ", fullname.Trim().Split(' ').Skip(1));
        }
    }
}