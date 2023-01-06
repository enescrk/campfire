using camp_fire.Application.IServices;
using camp_fire.Application.Models;
using camp_fire.Domain.Entities;
using camp_fire.Domain.SeedWork;
using camp_fire.Domain.SeedWork.Exceptions;

namespace camp_fire.Application.Services;

public class StoryService : IStoryService
{
    private readonly IUnitOfWork _unitOfWork;

    public StoryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<List<StoryResponseVM>> GetAsync(GetStorysRequestVM request)
    {
        var stories = _unitOfWork.GetRepository<Story>().Find(x =>

        request.Id == null || x.Id == request.Id
        && (request.Type == null || x.Type == request.Type))

        .Select(x => new StoryResponseVM
        {
            Id=x.Id,
            Text = x.Text,
            Type = x.Type
        }).ToList();

        return await Task.FromResult(stories);
    }
    public async Task<StoryResponseVM?> GetByIdAsync(int id)
    {
        var story = await _unitOfWork.GetRepository<Story>().GetByIdAsync(id);

        if (story is null)
            throw new ApiException("story couldn't find ");

        var mappedStory = MapStoryResposeVMHelper(story);

        return mappedStory;
    }

    public async Task<int> CreateAsync(CreateStoryRequestVM request)
    {
        var story = new Story
        {
            Text = request.Text,
            Type = request.Type
        };

        await _unitOfWork.GetRepository<Story>().CreateAsync(story);

        return await _unitOfWork.SaveChangesAsync();
    }

    public async Task<StoryResponseVM> UpdateAsync(UpdateStoryRequestVM request)
    {
        var story = await _unitOfWork.GetRepository<Story>().GetByIdAsync(request.Id!.Value);

        if (story is null)
            throw new ApiException("Story couldn't find");
        story.Text = request.Text;
        story.Type = request.Type;

        _unitOfWork.GetRepository<Story>().Update(story);

        await _unitOfWork.SaveChangesAsync();

        var newStory = await _unitOfWork.GetRepository<Story>().GetByIdAsync(request.Id!.Value);

        var mappedStory = MapStoryResposeVMHelper(newStory);

        return mappedStory;
    }

    public async Task DeleteAsync(int id)
    {
        var story = await _unitOfWork.GetRepository<Story>().GetByIdAsync(id);

        if (story is null)
            throw new ApiException("story couldn't find");

        _unitOfWork.GetRepository<Story>().Delete(story);
    }

    #region Helpers 

    private StoryResponseVM MapStoryResposeVMHelper(Story story)
    {
        var StoryResponseVM = new StoryResponseVM
        {
            Text = story.Text,
            Type = story.Type
        };
        return StoryResponseVM;
    }

    #endregion
}

