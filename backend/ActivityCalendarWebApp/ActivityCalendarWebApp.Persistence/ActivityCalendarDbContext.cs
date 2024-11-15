using ActivityCalendarWebApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ActivityCalendarWebApp.Persistence;

public class ActivityCalendarDbContext : DbContext
{
    public ActivityCalendarDbContext(DbContextOptions<ActivityCalendarDbContext> options) : base(options) {}
    
    public DbSet<Activity> Activities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Activity>(entity =>
        {
            entity.ToTable("Activity");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Description).IsRequired().HasMaxLength(150);
        });
    }
}