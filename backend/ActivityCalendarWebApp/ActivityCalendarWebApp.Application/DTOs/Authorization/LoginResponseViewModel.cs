namespace ActivityCalendarWebApp.Application.DTOs.Authorization
{
    public record LoginResponseViewModel(
        string AccessToken, 
        string RefreshToken);
}
