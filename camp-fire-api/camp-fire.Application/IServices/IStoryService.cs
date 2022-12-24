using camp_fire.Application.Models;
using camp_fire.Domain.Entities;

namespace camp_fire.Application.IServices;

public interface IStoryService
{
    Task<int> CreateAsync(Story request);
    Task<Story> UpdateAsync(Story request);
    Task<StoryResponseVM?> GetAsync(int id);
}
