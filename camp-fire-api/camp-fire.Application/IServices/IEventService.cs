using camp_fire.Application.Models;
using camp_fire.Domain.Entities;

namespace camp_fire.Application.IServices;

public interface IEventService
{
    Task<int> CreateAsync(Event request);
    Task<Event> UpdateAsync(Event request);
    Task<EventResponseVM?> GetAsync(int id);
}
