using ActivityCalendarWebApp.Application.DTOs.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IAuthorizationService = ActivityCalendarWebApp.Application.Interfaces.IAuthorizationService;

namespace ActivityCalendarWebApp.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AuthorizationController : ControllerBase
{
    private readonly IAuthorizationService _authorizationService;

    public AuthorizationController(IAuthorizationService authorizationService)
    {
        _authorizationService = authorizationService;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> RegisterUser(RegisterViewModel model)
    {
        await _authorizationService.RegisterAsync(model);
        return Ok();
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        var user = await _authorizationService.LoginAsync(model);
        return Ok(user);
    }

    [HttpPost("RefreshToken")]
    [Authorize]
    public async Task<IActionResult> RefreshToken()
    {
        var user = await _authorizationService.RefreshAsync();
        return Ok(user);
    }

    [HttpPost("Logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await _authorizationService.LogoutAsync();
        return Ok();
    }
}