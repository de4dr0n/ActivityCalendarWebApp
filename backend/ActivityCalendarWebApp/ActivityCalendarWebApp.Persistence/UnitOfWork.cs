using ActivityCalendarWebApp.Domain.Interfaces;
using ActivityCalendarWebApp.Persistence.Repositories;

namespace ActivityCalendarWebApp.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly ActivityCalendarDbContext _context;
    private ActivityRepository _activityRepository;
    private UserRepository _userRepository;

    public UnitOfWork(ActivityCalendarDbContext context)
    {
        _context = context;
    }
    
    public IActivityRepository Activities => _activityRepository ??= new ActivityRepository(_context);
    public IUserRepository Users => _userRepository ??= new UserRepository(_context);
    
    public void Dispose()
    {
        _context.Dispose();
    }
    
    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}