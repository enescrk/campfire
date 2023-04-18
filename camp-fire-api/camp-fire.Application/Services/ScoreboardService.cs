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

    public async Task<ScoreboardResponseVM> CreateAsync(CreateScoreboardRequestVM request)
    {
        var eventt = await _unitOfWork.GetRepository<Event>().GetByIdAsync(request.EventId);

        if (eventt is null)
            throw new NullReferenceException("Event could not be found!");

        var scoreboard = new Scoreboard
        {
            EventId = request.EventId,
            PageId = request.PageId,
            UserId = request.UserId,
            Score = request.Score
        };

        await _unitOfWork.GetRepository<Scoreboard>().CreateAsync(scoreboard);

        await _unitOfWork.SaveChangesAsync();

        var newScoreboard = await _unitOfWork.GetRepository<Scoreboard>().GetByIdAsync(request.Id);

        var mappedScoreboard = MapScoreboardResponseVMHelper(newScoreboard);

        return mappedScoreboard;
    }

    public Task<ScoreboardResponseVM?> GetAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<ScoreboardsByEventIdResponseVM>?> GetByEventIdAsync(GetScoreboardRequestVM request)
    {
        var eventt = await _unitOfWork.GetRepository<Event>().GetByIdAsync(request.EventId);

        if (eventt is null)
            throw new NullReferenceException("Event could not be found!");

        var scoreboards = _unitOfWork.GetRepository<Scoreboard>().Find(x => x.EventId == request.EventId).ToList();

        var result = scoreboards.GroupBy(x => new { x.PageId, x.UserId }).Select(x => new ScoreboardsByEventIdResponseVM
        {
            UserId = x.Key.UserId,
            PageId = x.Key.PageId,
            TotalScore = x.Sum(y => y.Score)
        }).ToList();

        return result;
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

    public async Task DeleteAsync(int id)
    {
        var scoreboard = await _unitOfWork.GetRepository<Scoreboard>().GetByIdAsync(id);

        if (scoreboard is null)
            throw new ApiException("Scoreboard couldn't find");

        _unitOfWork.GetRepository<Scoreboard>().Delete(scoreboard);
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
