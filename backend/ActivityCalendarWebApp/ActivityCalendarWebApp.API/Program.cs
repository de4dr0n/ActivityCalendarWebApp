using ActivityCalendarWebApp.API.Mappings;
using ActivityCalendarWebApp.API.Middleware;
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

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:3000");
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowCredentials();
    });
});
builder.Services.AddAutoMapper(typeof(ActivityMappingProfile));
builder.Services.AddMemoryCache();
builder.Services.AddLogging();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseRouting();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.Run();