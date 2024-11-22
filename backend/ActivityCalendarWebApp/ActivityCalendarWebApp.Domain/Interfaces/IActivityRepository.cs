using ActivityCalendarWebApp.Domain.Entities;

namespace ActivityCalendarWebApp.Domain.Interfaces;

public interface IActivityRepository
{
    Task<IEnumerable<Activity>> GetAllActivitiesAsync();
    Task<Activity?> GetActivityByIdAsync(Guid id);
    Task CreateActivityAsync(Activity activity);
    void UpdateActivity(Activity activity);
    void DeleteActivity(Activity activity);
    Task<IEnumerable<Activity>> GetActivitiesByUserAsync(Guid userId);
}