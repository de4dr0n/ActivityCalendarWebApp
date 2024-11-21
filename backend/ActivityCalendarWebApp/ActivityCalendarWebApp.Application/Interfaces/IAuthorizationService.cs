using ActivityCalendarWebApp.Application.DTOs.Authorization;

namespace ActivityCalendarWebApp.Application.Interfaces;

public interface IAuthorizationService
{
    Task RegisterAsync(RegisterViewModel model);
    Task<LoginResponseViewModel> LoginAsync(LoginViewModel model);
    Task LogoutAsync();
    Task<LoginResponseViewModel> RefreshAsync(RefreshTokenViewModel model);
}