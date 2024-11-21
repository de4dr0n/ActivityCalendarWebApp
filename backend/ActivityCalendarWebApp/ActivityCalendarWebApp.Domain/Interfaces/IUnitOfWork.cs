namespace ActivityCalendarWebApp.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IActivityRepository Activities { get; }
    IUserRepository Users { get; }
    Task<int> SaveChangesAsync();
}