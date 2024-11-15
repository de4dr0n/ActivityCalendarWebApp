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
    
    public async Task<IEnumerable<Activity>> GetAllActivities()
    {
        return await _context.Activities.AsNoTracking().ToListAsync();
    }

    public async Task<Activity> GetActivityById(Guid id)
    {
        return await _context.Activities.FindAsync(id);
    }

    public async Task CreateActivity(Activity activity)
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
}