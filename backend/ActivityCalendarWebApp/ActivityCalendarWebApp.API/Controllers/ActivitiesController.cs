using ActivityCalendarWebApp.Application.DTOs.Activity;
using ActivityCalendarWebApp.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ActivityCalendarWebApp.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ActivitiesController : ControllerBase
{
    private readonly IActivityService _activityService;

    public ActivitiesController(IActivityService activityService)
    {
        _activityService = activityService;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll()
    {
        var activities = await _activityService.GetAllActivitiesAsync();
        return Ok(activities);
    }

    [HttpGet("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> GetById(Guid id)
    {
        var activity = await _activityService.GetActivityByIdAsync(id);
        return Ok(activity);
    }

    [HttpGet("{date}")]
    [Authorize]
    public async Task<IActionResult> GetByDate(DateTime date)
    {
        var activities = await _activityService.GetActivitiesByDateAsync(date);
        return Ok(activities);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] ActivityCreateDto activity)
    {
        await _activityService.CreateActivityAsync(activity);
        return CreatedAtAction(nameof(GetByDate), new { date = activity.Date }, activity);
    }

    [HttpPut("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> Update(Guid id, [FromBody] ActivityUpdateDto activity)
    {
        await _activityService.UpdateActivityAsync(id, activity);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _activityService.DeleteActivityAsync(id);
        return NoContent();
    }
}