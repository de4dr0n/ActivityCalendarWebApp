using ActivityCalendarWebApp.Domain.Entities;

namespace ActivityCalendarWebApp.Application.DTOs.Activity;

public record ActivityCreateDto(
    DateTime Date,
    string Type,
    string? Description,
    decimal Progress);