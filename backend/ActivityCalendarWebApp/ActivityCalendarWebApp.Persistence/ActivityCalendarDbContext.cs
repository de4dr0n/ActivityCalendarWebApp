using ActivityCalendarWebApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ActivityCalendarWebApp.Persistence;

public class ActivityCalendarDbContext : DbContext
{
    public ActivityCalendarDbContext(DbContextOptions<ActivityCalendarDbContext> options) : base(options) {}
    
    public DbSet<Activity> Activities { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Activity>(entity =>
        {
            entity.HasKey(a => a.Id);
            entity.Property(a => a.Type).IsRequired();
            entity.Property(a => a.Date).IsRequired();
            entity.Property(a => a.Description).HasMaxLength(150);
            entity.Property(a => a.Progress).IsRequired().HasPrecision(18, 4);
            entity.Property(a => a.Status).IsRequired();
            
            entity.HasOne(a => a.User)
                .WithMany(u => u.Activities)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.HasIndex(u => u.Username).IsUnique();
        });
    }
}