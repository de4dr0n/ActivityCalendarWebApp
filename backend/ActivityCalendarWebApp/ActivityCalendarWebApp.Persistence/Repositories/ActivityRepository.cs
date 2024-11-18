using ActivityCalendarWebApp.Domain.Entities;
using ActivityCalendarWebApp.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ActivityCalendarWebApp.Persistence.Repositories;

public class ActivityRepository : IActivityRepository
{
    private readonly ActivityCalendarDbContext _context;
    
    public ActivityRepository(ActivityCalendarDbContext dbContext)
    {
        _context = dbContext;
    }
    
    public async Task<IEnumerable<Activity>> GetAllActivitiesAsync()
    {
        return await _context.Activities
            .AsNoTracking()
            .OrderBy(a => a.Date)
            .ToListAsync();
    }

    public async Task<Activity?> GetActivityByIdAsync(Guid id)
    {
        return await _context.Activities.FindAsync(id);
    }

    public async Task CreateActivityAsync(Activity activity)
    {
        await _context.Activities.AddAsync(activity);
    }

    public void UpdateActivity(Activity activity)
    {
        _context.Update(activity);
    }

    public void DeleteActivity(Activity activity)
    {
        _context.Activities.Remove(activity);
    }
    
    public async Task<IEnumerable<Activity>> GetActivitiesByDateAsync(DateTime date)
    {
        return await _context.Activities
            .Where(a => a.Date.Date == date.Date)
            .OrderBy(a => a.Date)
            .ToListAsync();
    }
}