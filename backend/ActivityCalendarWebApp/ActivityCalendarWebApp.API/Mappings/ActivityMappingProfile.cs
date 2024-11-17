using ActivityCalendarWebApp.Application.DTOs;
using ActivityCalendarWebApp.Application.DTOs.Activity;
using ActivityCalendarWebApp.Domain.Entities;
using AutoMapper;

namespace ActivityCalendarWebApp.API.Mappings;

public class ActivityMappingProfile : Profile
{
    public ActivityMappingProfile()
    {
        CreateMap<Activity, ActivityCreateDto>().ReverseMap();
        CreateMap<Activity, ActivityUpdateDto>().ReverseMap();
        CreateMap<Activity, ActivityViewModel>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
    }
}