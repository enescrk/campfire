using camp_fire.Application.Models;

namespace camp_fire.Application.IServices;

public interface IStoryService
{
    Task<int> CreateAsync(CreateStoryRequestVM request);
    Task<StoryResponseVM> UpdateAsync(UpdateStoryRequestVM request);
    Task<StoryResponseVM?> GetByIdAsync(int id);
    List<StoryResponseVM>? Getll();
}