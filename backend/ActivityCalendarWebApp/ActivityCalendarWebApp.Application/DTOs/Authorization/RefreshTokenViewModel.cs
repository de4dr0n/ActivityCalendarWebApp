namespace ActivityCalendarWebApp.Application.DTOs.Authorization
{
    public record RefreshTokenViewModel(
        string AccessToken,
        string RefreshToken);
}
