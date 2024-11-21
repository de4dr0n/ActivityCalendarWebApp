using System.Security.Claims;

namespace ActivityCalendarWebApp.Application.Interfaces;

public interface ITokenService
{
    string GenerateAccessToken(IEnumerable<Claim> claims);
    string GenerateRefreshToken();
}