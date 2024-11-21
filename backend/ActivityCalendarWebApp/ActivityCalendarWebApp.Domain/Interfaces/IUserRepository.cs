using ActivityCalendarWebApp.Domain.Entities;

namespace ActivityCalendarWebApp.Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> GetUserByIdAsync(Guid userId);
    Task<User?> GetUserByUsernameAsync(string username);
    Task AddUserAsync(User user);
    void UpdateUser(User user);
    void DeleteUser(User user);
    Task<IEnumerable<Activity>> GetActivitiesForUserAsync(Guid userId);
}