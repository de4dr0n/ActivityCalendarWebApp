using ActivityCalendarWebApp.Application.DTOs.User;

namespace ActivityCalendarWebApp.Application.Interfaces;

public interface IUserService
{
    Task<UserResponseViewModel> GetUserByIdAsync(Guid userId);
}