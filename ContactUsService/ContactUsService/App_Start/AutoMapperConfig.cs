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
                    .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => string.Join(" ", src.Customer.FirstName, src.Customer.LastName)))
                    .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.Text));

                cfg.CreateMap<Controllers.DTOs.ContactUsFormDTO, Models.CustomerMessage>()
                     .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => new Models.Customer() { FirstName = getFirst(src.FullName) , LastName = getLast(src.FullName), Email = src.Email }))
                     .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Message))
                     .ForMember(dest => dest.ReceivedOn, opt => opt.Ignore());
            });
        } 

        private static string getFirst(string fullname)
        {
            return fullname.Trim().Split(' ')[0];
        }
        private static string getLast(string fullname)
        {
            return string.Join(" ", fullname.Trim().Split(' ').Skip(1));
        }
    }
}