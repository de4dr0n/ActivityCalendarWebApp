using ActivityCalendarWebApp.Domain.Entities;

namespace ActivityCalendarWebApp.Application.DTOs.Activity;

public record ActivityCreateDto(
    DateTime Date,
    ActivityType Type,
    string? Description,
    decimal Progress);