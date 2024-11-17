namespace ActivityCalendarWebApp.Application.DTOs.Activity;

public record ActivityViewModel(
    Guid Id,
    DateTime Date,
    string Type,
    string? Description,
    decimal Progress,
    string Status);