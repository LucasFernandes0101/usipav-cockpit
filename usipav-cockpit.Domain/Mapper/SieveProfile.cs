using AutoMapper;
using usipav_cockpit.Domain.Entities;
using usipav_cockpit.Domain.ViewModels;

namespace usipav_cockpit.Domain.Mapper
{
    public class SieveProfile : Profile
    {
        public SieveProfile()
        {
            CreateMap<Sieve, SieveViewModel>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(x => x.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(x => x.MaxTemperature, opt => opt.MapFrom(src => src.MaxTemperature))
                .ForMember(x => x.MinTemperature, opt => opt.MapFrom(src => src.MinTemperature)).ReverseMap();
        }
    }
}
