namespace ActivityCalendarWebApp.Domain.Entities;

public class Activity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime Date { get; set; }
    public ActivityType Type { get; set; }
    public string? Description { get; set; }
    public decimal Progress { get; set; }
    public ActivityStatus Status { get; set; } = ActivityStatus.Planned;
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
}