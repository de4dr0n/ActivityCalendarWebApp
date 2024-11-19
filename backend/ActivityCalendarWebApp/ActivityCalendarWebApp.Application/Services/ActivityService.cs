using ActivityCalendarWebApp.Application.DTOs.Activity;
using ActivityCalendarWebApp.Application.Interfaces;
using ActivityCalendarWebApp.Domain.Entities;
using ActivityCalendarWebApp.Domain.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace ActivityCalendarWebApp.Application.Services;

public class ActivityService : IActivityService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _cache;
    private readonly ILogger<ActivityService> _logger;
    private const string CacheKey = "activities";

    public ActivityService(IUnitOfWork unitOfWork, IMapper mapper, IMemoryCache cache, ILogger<ActivityService> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _cache = cache;
        _logger = logger;
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
        _logger.LogInformation($"Fetching activities for date {date.ToShortDateString()}...");
        var datedActivities = await _unitOfWork.Activities.GetActivitiesByDateAsync(date);
        return _mapper.Map<IEnumerable<ActivityViewModel>>(datedActivities);
    }

    public async Task CreateActivityAsync(ActivityCreateDto model)
    {
        _logger.LogInformation("Creating a new activity...");
        var activity = _mapper.Map<Activity>(model);
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
        _unitOfWork.Activities.DeleteActivity(activityToDelete);
        await _unitOfWork.SaveChangesAsync();
        
        _logger.LogInformation("Invalidating activities cache after delete operation.");
        _cache.Remove(CacheKey);
    }
}