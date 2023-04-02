using camp_fire.Application.IServices;
using camp_fire.Application.Models;
using camp_fire.Domain.Entities;
using camp_fire.Domain.SeedWork;
using camp_fire.Domain.SeedWork.Exceptions;

namespace camp_fire.Application.Services;

public class PageService : IPageService
{
    private readonly IUnitOfWork _unitOfWork;

    public PageService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> CreateAsync(Page request)
    {
        await _unitOfWork.GetRepository<Page>().CreateAsync(request);

        return await _unitOfWork.SaveChangesAsync();
    }

    public async Task<Page> UpdateAsync(PageResponseVM request)
    {
        var page = await _unitOfWork.GetRepository<Page>().GetByIdAsync(request.Id);

        if (page is null)
            throw new ApiException("Event couldn't find");

        page.Name = request.Name;
        page.IsCompleted = request.IsCompleted;

        _unitOfWork.GetRepository<Page>().Update(page);

        await _unitOfWork.SaveChangesAsync();

        var newPage = await _unitOfWork.GetRepository<Page>().GetByIdAsync(request.Id);

        return newPage;
    }

    public async Task UpdateIsCompleteAsync(UpdatePageIsCompleteRequestVM request)
    {
        var page = await _unitOfWork.GetRepository<Page>().GetByIdAsync(request.Id);

        page.IsCompleted = request.IsCompleted;

        _unitOfWork.GetRepository<Page>().Update(page);
    }

    public async Task DeleteAsync(int id)
    {
        var page = await _unitOfWork.GetRepository<Page>().GetByIdAsync(id);

        if (page is null)
            throw new ApiException("page couldn't find");

        _unitOfWork.GetRepository<Page>().Delete(page);
    }

    public async Task<List<PageResponseVM>> GetAsync(GetPagesRequestVM request)
    {
        var pages = _unitOfWork.GetRepository<Page>().Find(x =>
        request.Id == null || x.Id == request.Id
        && (request.EventId == null || x.EventId == request.EventId)
        && (string.IsNullOrEmpty(request.Name) || x.Name!.ToLower() == request.Name.ToLower().Trim())
        && (request.ScoreboardId == null || x.ScoreboardId == request.ScoreboardId))
        .Select(x => new PageResponseVM
        {
            Id = x.Id,
            Name = x!.Name!,
            EventId = x.EventId,
            EventName = x.Event!.Name,
            ScoreboardId = x.Scoreboard!.Id
        }).ToList();

        return await Task.FromResult(pages);
    }

    public async Task<PageResponseVM?> GetByIdAsync(int id)
    {
        var page = _unitOfWork.GetRepository<Page>().FindOne(x => x.Id == id);

        if (page is null)
            throw new ApiException("page couldn't find ");

        var result = new PageResponseVM
        {
            Id = page.Id,
            Name = page!.Name!,
            EventId = page.EventId,
            EventName = page.Event?.Name,
            ScoreboardId = page.Scoreboard?.Id,
            IsCompleted = page.IsCompleted
        };

        return await Task.FromResult(result);
    }

    public async Task SaveChangesAsync()
    {
        await _unitOfWork.SaveChangesAsync();
    }
}
