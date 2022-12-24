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

    public async Task<Page> UpdateAsync(Page request)
    {
        var pagee= await _unitOfWork.GetRepository<Page>().GetByIdAsync(request.Id);

        if (pagee is null)
            throw new ApiException("Event couldn't find");

        pagee.Name = request.Name;
        pagee.User.Name = request?.User?.Name;

        _unitOfWork.GetRepository<Page>().Update(pagee);

        await _unitOfWork.SaveChangesAsync();

        var newPage = await _unitOfWork.GetRepository<Page>().GetByIdAsync(request.Id);

        return newPage;
    }

    public async Task<PageResponseVM?> GetAsync(int id)
    {
        var pagee = _unitOfWork.GetRepository<Page>().FindOne(x => x.Id == id);

        var result = new PageResponseVM
        {
            Name = pagee!.Name!,
            EventId=pagee.EventId,
            Event = pagee?.Event,
            Scoreboard = pagee?.Scoreboard
        };

        return await Task.FromResult(result);
    }
}
