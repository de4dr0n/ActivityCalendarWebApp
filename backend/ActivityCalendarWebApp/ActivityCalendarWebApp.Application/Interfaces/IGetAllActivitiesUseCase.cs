using ActivityCalendarWebApp.Application.DTOs;

namespace ActivityCalendarWebApp.Application.Interfaces;

public interface IGetAllActivitiesUseCase
{
    public Task<List<ActivityViewModel>> ExecuteAsync();
}