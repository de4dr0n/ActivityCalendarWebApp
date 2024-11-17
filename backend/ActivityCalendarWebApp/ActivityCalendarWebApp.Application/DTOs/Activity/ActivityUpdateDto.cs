using ActivityCalendarWebApp.Domain.Entities;

namespace ActivityCalendarWebApp.Application.DTOs.Activity;

public record ActivityUpdateDto(
    Guid Id,
    DateTime Date,
    ActivityType Type,
    string? Description,
    decimal Progress,
    ActivityStatus Status);