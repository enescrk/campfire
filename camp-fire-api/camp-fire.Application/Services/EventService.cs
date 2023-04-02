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

    public async Task<EventResponseVM> UpdateCurrentPageAsync(UpdatePageRequestVM request)
    {
        var eventt = await _unitOfWork.GetRepository<Event>().GetByIdAsync(request.EventId);

        if (eventt is null)
            throw new NullReferenceException("Event couldn't find");

        var page = eventt.Pages.FirstOrDefault(x => x.Id == request.PageId);

        if (page is null)
            throw new ApiException("Page couldn't find");

        page.IsCompleted = true;

        eventt.CurrentPageId = eventt!.Pages!.FirstOrDefault(x => !x.IsCompleted)!.Id;

        _unitOfWork.GetRepository<Page>().Update(page);
        _unitOfWork.GetRepository<Event>().Update(eventt);

        await _unitOfWork.SaveChangesAsync();

        var mappedEvent = MapEventResposeVMHelper(eventt);

        return mappedEvent;
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

    public async Task<List<EventResponseVM>?> GetAsync(GetEventsRequestVM request)
    {
        var eventList = _unitOfWork.GetRepository<Event>().Find(x => !x.IsDeleted
        && request.Id == null || x.Id == request.Id
        && request.Date == null || x.Date == request.Date
        && request.UserId == null || x.UserId == request.UserId
        && request.CurrentUserId == null || x.CurrentUserId == request.CurrentUserId
        && request.CurrentPageId == null || x.CurrentPageId == request.CurrentPageId
        && request.CompanyId == null || x.CompanyId == request.CompanyId
        && request.ParticipiantId == null || (x.ParticipiantIds != null && x.ParticipiantIds.Contains(request.ParticipiantId!.Value))
        && string.IsNullOrEmpty(request.Name) || x.Name == request.Name!.ToLower().Trim()
        && string.IsNullOrEmpty(request.HashedKey) || x.HashedKey == request.HashedKey!.ToLower().Trim()
        && string.IsNullOrEmpty(request.MeetingUrl) || x.MeetingUrl == request.MeetingUrl!.ToLower().Trim())
        .Select(x => new EventResponseVM
        {
            Id = x.Id,
            HashedKey = x.HashedKey,
            Name = x.Name,
            Date = x.Date,
            User = new UserResponseVM
            {
                Id = x.UserId,
                Name = x.User.Name,
                Surname = x.User.Surname
            },
            ParticipiantIds = x.ParticipiantIds.ToList(),
            PageIds = x.Pages.Select(y => x.Id).ToList(),
            Pages = x.Pages.Select(y => new PageResponseVM
            {
                Id = y.Id,
                IsCompleted = y.IsCompleted,
                Name = y.Name,
                ScoreboardId = y.Scoreboard.Id
            }).ToList(),
            Scoreboards = x.Scoreboards.Select(y => new ScoreboardResponseVM
            {
                Id = y.Id,
                EventId = y.EventId,
                PageId = y.PageId,
                UserId = y.UserId,
                Score = y.Score
            }).ToList()
        }).ToList();

        return await Task.FromResult(eventList);
    }

    public async Task DeleteAsync(int id)
    {
        var eventt = await _unitOfWork.GetRepository<Event>().GetByIdAsync(id);

        if (eventt is null)
            throw new ApiException("event couldn't find");

        _unitOfWork.GetRepository<Event>().Delete(eventt);
    }

    public async Task SaveChangesAsync()
    {
        await _unitOfWork.SaveChangesAsync();
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