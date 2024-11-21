using ActivityCalendarWebApp.Application.DTOs.User;
using ActivityCalendarWebApp.Application.Interfaces;
using ActivityCalendarWebApp.Domain.Interfaces;
using AutoMapper;

namespace ActivityCalendarWebApp.Application.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UserService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<UserResponseViewModel> GetUserByIdAsync(Guid userId)
    {
        var user = await _unitOfWork.Users.GetUserByIdAsync(userId);
        return _mapper.Map<UserResponseViewModel>(user);
    }
}