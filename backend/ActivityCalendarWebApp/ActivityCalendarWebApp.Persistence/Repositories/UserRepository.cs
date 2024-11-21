using ActivityCalendarWebApp.Domain.Entities;
using ActivityCalendarWebApp.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ActivityCalendarWebApp.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ActivityCalendarDbContext _context;

    public UserRepository(ActivityCalendarDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetUserByIdAsync(Guid userId)
    {
        return await _context.Users.FindAsync(userId);
    }

    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        return await _context.Users.Include(u => u.Activities).FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task AddUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public void UpdateUser(User user)
    {
        _context.Users.Update(user);
    }

    public void DeleteUser(User user)
    {
        _context.Users.Remove(user);
    }

    public async Task<IEnumerable<Activity>> GetActivitiesForUserAsync(Guid userId)
    {
        var user = await _context.Users.Include(u => u.Activities).FirstOrDefaultAsync(u => u.Id == userId);
        return user?.Activities ?? Enumerable.Empty<Activity>();
    }
}