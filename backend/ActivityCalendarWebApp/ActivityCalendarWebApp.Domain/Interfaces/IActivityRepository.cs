using ActivityCalendarWebApp.Domain.Entities;

namespace ActivityCalendarWebApp.Domain.Interfaces;

public interface IActivityRepository
{
    public Task<IEnumerable<Activity>> GetAllActivities();
    public Task<Activity> GetActivityById(Guid id);
    public Task CreateActivity(Activity activity);
    public void UpdateActivity(Activity activity);
    public void DeleteActivity(Activity activity);
}