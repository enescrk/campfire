using camp_fire.Application.Models;
using camp_fire.Application.Models.Request;
using camp_fire.Domain.Entities;

namespace camp_fire.Application.IServices;

public interface IEventService : IBaseService
{
    Task<int> CreateAsync(CreateEventReqeustVM request);
    Task<EventResponseVM> UpdateAsync(UpdateEventRequestVM request);
    Task<EventResponseVM?> GetAsync(int id);
    Task<List<EventResponseVM>?> GetAsync(GetEventsRequestVM request);
    Task<EventResponseVM> UpdateCurrentPageAsync(UpdatePageRequestVM request);
    Task<EventResponseVM> UpdateActiveUserAsync(ActivateUserRequestVM request);
}
