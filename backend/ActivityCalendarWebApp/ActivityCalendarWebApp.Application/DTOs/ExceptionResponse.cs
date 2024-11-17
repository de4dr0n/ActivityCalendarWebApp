using System.Net;

namespace ActivityCalendarWebApp.Application.DTOs
{
    public record ExceptionResponse(HttpStatusCode StatusCode, string Description);
}
