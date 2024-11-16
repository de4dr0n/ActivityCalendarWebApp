using ActivityCalendarWebApp.Application.DTOs;
using ActivityCalendarWebApp.Domain.Entities;
using AutoMapper;

namespace ActivityCalendarWebApp.API.Mappings;

public class ActivityMappingProfile : Profile
{
    public ActivityMappingProfile()
    {
        CreateMap<Activity, ActivityViewModel>().ReverseMap();
    }
}