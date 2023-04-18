using camp_fire.Application.Models;
using camp_fire.Domain.Entities;

namespace camp_fire.Application.IServices;

public interface IScoreboardService
{
    Task<ScoreboardResponseVM> UpdateAsync(UpdateScoreboardRequestVM request);
    Task<ScoreboardResponseVM> CreateAsync(CreateScoreboardRequestVM request);
    Task<List<ScoreboardsByEventIdResponseVM>?> GetByEventIdAsync(GetScoreboardRequestVM request);
    Task<ScoreboardResponseVM?> GetAsync(int id);
}
