using camp_fire.Application.Models;

namespace camp_fire.Application.IServices;

public interface IExperienceService
{
    Task<int> CreateAsync(CreateExperienceRequest request);
    Task<ExperienceResponse> UpdateAsync(UpdateExperienceRequest request);
    // Task<StoryResponseVM?> GetByIdAsync(int id);
    Task<List<ExperienceResponse>>? GetllAsync(GetExperienceRequest request);
}