using ActivityCalendarWebApp.Application.DTOs.Activity;
using ActivityCalendarWebApp.Application.Interfaces;
using ActivityCalendarWebApp.Domain.Entities;
using ActivityCalendarWebApp.Domain.Interfaces;
using AutoMapper;

namespace ActivityCalendarWebApp.Application.Services;

public class ActivityService : IActivityService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ActivityService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<ActivityViewModel>> GetAllActivitiesAsync()
    {
        var activities = await _unitOfWork.Activities.GetAllActivitiesAsync();
        return _mapper.Map<IEnumerable<ActivityViewModel>>(activities);
    }

    public async Task<ActivityViewModel> GetActivityByIdAsync(Guid id)
    {
        var activity = await _unitOfWork.Activities.GetActivityByIdAsync(id);
        if (activity == null) throw new KeyNotFoundException($"Activity with id: {id} not found");
        return _mapper.Map<ActivityViewModel>(activity);
    }
    
    public async Task<IEnumerable<ActivityViewModel>> GetActivitiesByDateAsync(DateTime date)
    {
        var datedActivities = await _unitOfWork.Activities.GetActivitiesByDateAsync(date);
        return _mapper.Map<IEnumerable<ActivityViewModel>>(datedActivities);
    }

    public async Task CreateActivityAsync(ActivityCreateDto model)
    {
        var activity = _mapper.Map<Activity>(model);
        await _unitOfWork.Activities.CreateActivityAsync(activity);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateActivityAsync(Guid id, ActivityUpdateDto model)
    {
        if (id != model.Id) throw new ApplicationException();
        var activityToUpdate = await _unitOfWork.Activities.GetActivityByIdAsync(id);
        if (activityToUpdate == null) throw new KeyNotFoundException("Activity not found");
        
        _mapper.Map(model, activityToUpdate);
        activityToUpdate.Id = model.Id;
        _unitOfWork.Activities.UpdateActivity(activityToUpdate);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteActivityAsync(Guid id)
    {
        var activityToDelete = await _unitOfWork.Activities.GetActivityByIdAsync(id);
        if (activityToDelete == null) throw new KeyNotFoundException("Activity not found");
        _unitOfWork.Activities.DeleteActivity(activityToDelete);
        await _unitOfWork.SaveChangesAsync();
    }
}