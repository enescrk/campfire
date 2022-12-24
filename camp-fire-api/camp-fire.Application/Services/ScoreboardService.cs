using camp_fire.Application.IServices;
using camp_fire.Application.Models;
using camp_fire.Domain.Entities;
using camp_fire.Domain.SeedWork;
using camp_fire.Domain.SeedWork.Exceptions;

namespace camp_fire.Application.Services;

public class ScoreboardService : IScoreboardService
{
    private readonly IUnitOfWork _unitOfWork;

    public ScoreboardService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<int> CreateAsync(Scoreboard request)
    {
        throw new NotImplementedException();
    }

    public Task<ScoreboardResponseVM?> GetAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<ScoreboardResponseVM> UpdateAsync(UpdateScoreboardRequestVM request)
    {
        var scoreboard = await _unitOfWork.GetRepository<Scoreboard>().GetByIdAsync(request.Id);

        if (scoreboard is null)
            throw new ApiException("Scoreboard couldn't find");

        scoreboard.Score = request.Score;

        _unitOfWork.GetRepository<Scoreboard>().Update(scoreboard);

        await _unitOfWork.SaveChangesAsync();

        var newScoreboard = await _unitOfWork.GetRepository<Scoreboard>().GetByIdAsync(request.Id);

        var mappedScoreboard = MapScoreboardResponseVMHelper(newScoreboard);

        return mappedScoreboard;
    }

    #region Helpers

    private ScoreboardResponseVM MapScoreboardResponseVMHelper(Scoreboard scoreboard)
    {
        var scoreboardResponseVM = new ScoreboardResponseVM
        {
            Id = scoreboard.Id,
            EventId = scoreboard.EventId,
            PageId = scoreboard.PageId,
            Score = scoreboard.Score
        };

        return scoreboardResponseVM;
    }

    #endregion
}
