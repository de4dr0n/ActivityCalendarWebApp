using ActivityCalendarWebApp.Application.DTOs.Activity;

namespace ActivityCalendarWebApp.Application.Interfaces;

public interface IActivityService
{
    Task<IEnumerable<ActivityViewModel>> GetAllActivitiesAsync();
    Task<ActivityViewModel> GetActivityByIdAsync(Guid id);
    Task<IEnumerable<ActivityViewModel>> GetActivitiesByUserAsync();
    Task CreateActivityAsync(ActivityCreateDto model);
    Task UpdateActivityAsync(Guid id, ActivityUpdateDto model);
    Task DeleteActivityAsync(Guid id);
}
