using camp_fire.Application.IServices;
using camp_fire.Application.Models;
using camp_fire.Domain.Entities;
using camp_fire.Domain.SeedWork;
using camp_fire.Domain.SeedWork.Exceptions;

namespace camp_fire.Application.Services;

public class EventService : IEventService
{
    private readonly IUnitOfWork _unitOfWork;

    public EventService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> CreateAsync(Event request)
    {
        await _unitOfWork.GetRepository<Event>().CreateAsync(request);

        return await _unitOfWork.SaveChangesAsync();
    }

    public async Task<Event> UpdateAsync(Event request)
    {
        var eventt = await _unitOfWork.GetRepository<Event>().GetByIdAsync(request.Id);

        if (eventt is null)
            throw new ApiException("Event couldn't find");

        eventt.Name = request.Name;
        eventt.User.Name = request.User.Name;

        _unitOfWork.GetRepository<Event>().Update(eventt);

        await _unitOfWork.SaveChangesAsync();

        var newEvent = await _unitOfWork.GetRepository<Event>().GetByIdAsync(request.Id);

        return newEvent;
    }

    public async Task<EventResponseVM?> GetAsync(int id)
    {
        var eventt = _unitOfWork.GetRepository<Event>().FindOne(x => x.Id == id);

        var result = new EventResponseVM
        {
            Id = eventt.Id,
            HashedKey = eventt!.HashedKey!,
            Name = eventt!.Name!,
            UserName = eventt?.User,
            Pages = eventt.Pages?.ToList(),
            Scoreboards = eventt.Scoreboards?.ToList()
        };

        return await Task.FromResult(result);
    }
}