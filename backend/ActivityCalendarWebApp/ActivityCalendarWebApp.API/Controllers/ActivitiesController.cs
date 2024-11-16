using ActivityCalendarWebApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ActivityCalendarWebApp.API.Controllers;
[ApiController]
[Route("[controller]")]
public class ActivitiesController : ControllerBase
{
    private readonly IGetAllActivitiesUseCase _getAllActivities;

    public ActivitiesController(IGetAllActivitiesUseCase getAllActivitiesUseCase)
    {
        _getAllActivities = getAllActivitiesUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllActivities()
    {
        return Ok(await _getAllActivities.ExecuteAsync());
    }
}