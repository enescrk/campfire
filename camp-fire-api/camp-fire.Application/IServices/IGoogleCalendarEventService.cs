using camp_fire.Application.Models.Request;
using Google.Apis.Calendar.v3.Data;

namespace camp_fire.Application.IServices;

public interface IGoogleCalendarEventService
{
    Task<Event> CreateEventAsync(CreateGoogleCalendarEventVM request);
}