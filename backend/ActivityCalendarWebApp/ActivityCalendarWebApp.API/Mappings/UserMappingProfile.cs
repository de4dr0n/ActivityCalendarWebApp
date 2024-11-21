using ActivityCalendarWebApp.Application.DTOs.User;
using ActivityCalendarWebApp.Domain.Entities;
using AutoMapper;

namespace ActivityCalendarWebApp.API.Mappings;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<UserResponseViewModel, User>().ReverseMap();
    }
}