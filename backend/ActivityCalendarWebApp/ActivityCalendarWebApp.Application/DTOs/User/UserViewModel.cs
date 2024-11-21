using ActivityCalendarWebApp.Application.DTOs.Activity;

namespace ActivityCalendarWebApp.Application.DTOs.User;

public record UserResponseViewModel(
    Guid Id, 
    string Username, 
    string PasswordHash, 
    string? RefreshToken,
    DateTime RefreshTokenExpiry, 
    ICollection<ActivityViewModel>? Activities);