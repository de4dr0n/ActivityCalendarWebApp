using ActivityCalendarWebApp.API.Mappings;
using ActivityCalendarWebApp.Application.Interfaces;
using ActivityCalendarWebApp.Application.Services;
using ActivityCalendarWebApp.Domain.Interfaces;
using ActivityCalendarWebApp.Persistence;
using ActivityCalendarWebApp.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

#region DbContext

builder.Services.AddDbContext<ActivityCalendarDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

#endregion

#region Services

#region Repositories

builder.Services.AddScoped<IActivityRepository, ActivityRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

#endregion Repositories

#region Business Services

builder.Services.AddScoped<IActivityService, ActivityService>();

#endregion Business Services

#endregion Services

builder.Services.AddAutoMapper(typeof(ActivityMappingProfile));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseRouting();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();