using ActivityCalendarWebApp.Application.DTOs;
using ActivityCalendarWebApp.Application.Interfaces;
using ActivityCalendarWebApp.Domain.Interfaces;
using AutoMapper;

namespace ActivityCalendarWebApp.Application.UseCases;

public class GetAllActivitiesUseCase : IGetAllActivitiesUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllActivitiesUseCase(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<ActivityViewModel>> ExecuteAsync()
    {
        var activities = await _unitOfWork.Activities.GetAllActivities();
        return _mapper.Map<List<ActivityViewModel>>(activities);
    }
}