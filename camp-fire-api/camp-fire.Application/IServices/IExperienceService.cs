using camp_fire.Application.Models;

namespace camp_fire.Application.IServices;

public interface IExperienceService
{
    Task CreateAsync(CreateExperienceRequest request);
    Task<ExperienceResponse> UpdateAsync(UpdateExperienceRequest request);
    Task<ExperienceResponse?> GetByIdAsync(int id);
    Task<List<ExperienceResponse>>? GetllAsync(GetExperienceRequest request);
    Task<ExperienceResponse> AddModerator(AddModeratorRequest request);
    Task<ExperienceResponse> AddBox(AddBoxRequest request);
    Task DeleteAsync(int id);
}