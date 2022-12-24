using camp_fire.Application.Models;
using camp_fire.Domain.Entities;

namespace camp_fire.Application.IServices;

public interface IScoreboardService
{
    Task<int> CreateAsync(Scoreboard request);
    Task<ScoreboardResponseVM> UpdateAsync(UpdateScoreboardRequestVM request);
    Task<ScoreboardResponseVM?> GetAsync(int id);
}
