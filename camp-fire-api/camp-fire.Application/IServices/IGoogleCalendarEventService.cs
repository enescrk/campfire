using camp_fire.Application.Models.Request;

namespace camp_fire.Application.IServices;

public interface IGoogleCalendarEventService
{
    Task<string> CreateEventAsync(CreateGoogleCalendarEventVM request);
}