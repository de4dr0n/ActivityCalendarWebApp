namespace ActivityCalendarWebApp.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IActivityRepository Activities { get; }
    Task<int> SaveChangesAsync();
}