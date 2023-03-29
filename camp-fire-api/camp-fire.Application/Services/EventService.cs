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

    public async Task<EventResponseVM> UpdateAsync(UpdateEventRequestVM request)
    {
        var eventt = await _unitOfWork.GetRepository<Event>().GetByIdAsync(request.Id);

        if (eventt is null)
            throw new ApiException("Event couldn't find");

        eventt.Name = request.Name;
        eventt.Date = request.Date;
        eventt.HashedKey = request.HashedKey;
        eventt.MeetingUrl = request.MeetingUrl;
        eventt.ParticipiantIds = request.ParticipiantIds;
        eventt.PageIds = request.PageIds;
        eventt.CompanyId = request.CompanyId;

        _unitOfWork.GetRepository<Event>().Update(eventt);

        await _unitOfWork.SaveChangesAsync();

        var newEvent = await _unitOfWork.GetRepository<Event>().GetByIdAsync(request.Id);

        var mappedEvent = MapEventResposeVMHelper(newEvent);

        return mappedEvent;
    }

    public async Task UpdateCurrentPageAsync(UpdateEventRequestVM request)
    {
        var eventt = await _unitOfWork.GetRepository<Event>().GetByIdAsync(request.Id);

        if (eventt is null)
            throw new ApiException("Event couldn't find");

        eventt.CurrentPageId = request.CurrentPageId;

        _unitOfWork.GetRepository<Event>().Update(eventt);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<EventResponseVM?> GetAsync(int id)
    {
        var eventt = _unitOfWork.GetRepository<Event>().FindOne(x => x.Id == id);

        var result = new EventResponseVM
        {
            Id = eventt.Id,
            HashedKey = eventt.HashedKey,
            Name = eventt.Name,
            Date = eventt.Date,
            User = new UserResponseVM
            {
                Id = eventt.UserId,
                Name = eventt.User?.Name,
                Surname = eventt.User?.Surname
            },
            ParticipiantIds = eventt.ParticipiantIds?.ToList(),
            PageIds = eventt.Pages?.Select(x => x.Id).ToList(),
            Pages = eventt.Pages?.Select(x => new PageResponseVM
            {
                Id = x.Id,
                IsCompleted = x.IsCompleted,
                Name = x.Name,
                ScoreboardId = x.Scoreboard?.Id
            }).ToList(),
            Scoreboards = eventt.Scoreboards?.Select(x => new ScoreboardResponseVM
            {
                Id = x.Id,
                EventId = x.EventId,
                PageId = x.PageId,
                UserId = x.UserId,
                Score = x.Score
            }).ToList()
        };

        return await Task.FromResult(result);
    }

    public async Task DeleteAsync(int id)
    {
        var eventt = await _unitOfWork.GetRepository<Event>().GetByIdAsync(id);

        if (eventt is null)
            throw new ApiException("event couldn't find");

        _unitOfWork.GetRepository<Event>().Delete(eventt);
    }
    #region Helpers 

    private EventResponseVM MapEventResposeVMHelper(Event eventt)
    {
        var eventResponseVM = new EventResponseVM
        {
            Id = eventt.Id,
            Name = eventt.Name,
            Date = eventt.Date,
            HashedKey = eventt.HashedKey,
            PageIds = eventt.Pages.Select(x => x.Id).ToList(),
            Pages = eventt.Pages?.Select(x => new PageResponseVM
            {
                Id = x.Id,
                IsCompleted = x.IsCompleted,
                Name = x.Name,
                ScoreboardId = x.Scoreboard?.Id
            }).ToList(),
            User = new UserResponseVM
            {
                Id = eventt.UserId,
                Name = eventt.User?.Name,
                Surname = eventt.User?.Surname
            },
            Scoreboards = eventt.Scoreboards?.Select(x => new ScoreboardResponseVM
            {
                Id = x.Id,
                EventId = x.EventId,
                PageId = x.PageId,
                UserId = x.UserId,
                Score = x.Score
            }).ToList()
        };

        return eventResponseVM;
    }

    #endregion
}