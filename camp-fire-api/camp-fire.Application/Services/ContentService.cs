using camp_fire.Application.IServices;
using camp_fire.Application.Models.Request;
using camp_fire.Domain.Entities;
using camp_fire.Domain.SeedWork;
using camp_fire.Domain.SeedWork.Exceptions;

namespace camp_fire.Application.Services;

public class ContentService : IContentService
{ 
    private readonly IUnitOfWork _unitOfWork;

    public ContentService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<Content>> GetAsync(GetContentRequestVM request)
    {
        var content = _unitOfWork.GetRepository<Content>().Find(x =>
        request.Id == null || x.Id == request.Id
        && (request.EventId == null || x.EventId == request.EventId)).ToList();

        return await Task.FromResult(content);
    }

    public async Task<Content?> GetByIdAsync(int id)
    {
        var content = await _unitOfWork.GetRepository<Content>().GetByIdAsync(id);

        if (content is null)
            throw new ApiException("story couldn't find ");

        // var mappedGame = MapGameResposeVMHelper(game);

        return content;
    }

    public async Task<int> CreateAsync(Content request)
    {
        var content = new Content
        {
            EventId = request.EventId,
            Data = request.Data
        };

        await _unitOfWork.GetRepository<Content>().CreateAsync(content);

        return await _unitOfWork.SaveChangesAsync();
    }

    public async Task<Content> UpdateAsync(Content request)
    {
        var content = await _unitOfWork.GetRepository<Content>().GetByIdAsync(request.Id);

        if (content is null)
            throw new ApiException("Game couldn't find");
        content.Data = request.Data;

        _unitOfWork.GetRepository<Content>().Update(content);

        await _unitOfWork.SaveChangesAsync();

        var newContent = await GetByIdAsync(request.Id);

        return newContent!;
    }

    public async Task DeleteAsync(int id)
    {
        var content = await _unitOfWork.GetRepository<Content>().GetByIdAsync(id);

        if (content is null)
            throw new ApiException("content couldn't find");

        _unitOfWork.GetRepository<Content>().Delete(content);
    }

    #region Helpers 

    // private GameResponseVM MapGameResposeVMHelper(Content game)
    // {
    //     var gameResponseVM = new GameResponseVM
    //     {
    //         Id = game.Id,
    //     };

    //     return gameResponseVM;
    // }

    #endregion
}

