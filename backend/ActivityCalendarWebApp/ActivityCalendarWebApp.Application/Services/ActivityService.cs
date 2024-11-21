using System.Security.Claims;
using ActivityCalendarWebApp.Application.DTOs.Activity;
using ActivityCalendarWebApp.Application.Interfaces;
using ActivityCalendarWebApp.Domain.Entities;
using ActivityCalendarWebApp.Domain.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace ActivityCalendarWebApp.Application.Services;

public class ActivityService : IActivityService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _cache;
    private readonly ILogger<ActivityService> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private const string CacheKey = "activities";

    public ActivityService(IUnitOfWork unitOfWork, IMapper mapper, IMemoryCache cache, ILogger<ActivityService> logger, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _cache = cache;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<IEnumerable<ActivityViewModel>> GetAllActivitiesAsync()
    {
        _logger.LogInformation("Fetching all activities from cache or database...");
        return (await _cache.GetOrCreateAsync(CacheKey, async entry =>
        {
            _logger.LogInformation("Cache miss for activities. Fetching from database...");
            entry.SlidingExpiration = TimeSpan.FromMinutes(10);
            var activities = await _unitOfWork.Activities.GetAllActivitiesAsync();
            return _mapper.Map<IEnumerable<ActivityViewModel>>(activities);
        }))!;
    }

    public async Task<ActivityViewModel> GetActivityByIdAsync(Guid id)
    {
        _logger.LogInformation($"Fetching activity with ID {id}...");
        var activity = await _unitOfWork.Activities.GetActivityByIdAsync(id);
        if (activity == null) throw new KeyNotFoundException($"Activity with id: {id} not found");
        return _mapper.Map<ActivityViewModel>(activity);
    }
    
    public async Task<IEnumerable<ActivityViewModel>> GetActivitiesByDateAsync(DateTime date)
    {
        var userId = GetCurrentUserId();
        _logger.LogInformation($"Fetching activities for date {date.ToShortDateString()}...");
        var datedActivities = await _unitOfWork.Activities.GetActivitiesByDateAsync(date, userId);
        return _mapper.Map<IEnumerable<ActivityViewModel>>(datedActivities);
    }

    public async Task CreateActivityAsync(ActivityCreateDto model)
    {
        var userId = GetCurrentUserId();
        
        _logger.LogInformation("Creating a new activity...");
        var activity = _mapper.Map<Activity>(model);
        activity.UserId = userId;
        await _unitOfWork.Activities.CreateActivityAsync(activity);
        await _unitOfWork.SaveChangesAsync();
        
        _logger.LogInformation("Invalidating activities cache after create operation.");
        _cache.Remove(CacheKey);
    }

    public async Task UpdateActivityAsync(Guid id, ActivityUpdateDto model)
    {
        _logger.LogInformation($"Updating activity with ID {id}...");
        if (id != model.Id) throw new ApplicationException();
        var activityToUpdate = await _unitOfWork.Activities.GetActivityByIdAsync(id);
        if (activityToUpdate == null) throw new KeyNotFoundException("Activity not found");
        if (!ValidateUserId(activityToUpdate)) throw new AccessViolationException();
        
        _mapper.Map(model, activityToUpdate);
        activityToUpdate.Id = model.Id;
        _unitOfWork.Activities.UpdateActivity(activityToUpdate);
        await _unitOfWork.SaveChangesAsync();
        
        _logger.LogInformation("Invalidating activities cache after update operation.");
        _cache.Remove(CacheKey);
    }

    public async Task DeleteActivityAsync(Guid id)
    {
        _logger.LogInformation($"Deleting activity with ID {id}...");
        var activityToDelete = await _unitOfWork.Activities.GetActivityByIdAsync(id);
        if (activityToDelete == null) throw new KeyNotFoundException("Activity not found");
        if (!ValidateUserId(activityToDelete)) throw new AccessViolationException();

        _unitOfWork.Activities.DeleteActivity(activityToDelete);
        await _unitOfWork.SaveChangesAsync();
        
        _logger.LogInformation("Invalidating activities cache after delete operation.");
        _cache.Remove(CacheKey);
    }
    
    private Guid GetCurrentUserId()
    {
        if (_httpContextAccessor.HttpContext == null)
            throw new UnauthorizedAccessException("HTTP context is not available.");

        var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst("UserId")?.Value;

        if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            throw new UnauthorizedAccessException("User is not authorized or UserId is invalid.");

        return userId;
    }

    private bool ValidateUserId(Activity activity)
    {
        var userId = GetCurrentUserId();
        return userId == activity.UserId;
    }
}