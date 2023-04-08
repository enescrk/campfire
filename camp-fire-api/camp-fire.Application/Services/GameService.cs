using camp_fire.Application.IServices;
using camp_fire.Application.Models.Request;
using camp_fire.Application.Models.Response;
using camp_fire.Domain.Entities;
using camp_fire.Domain.SeedWork;
using camp_fire.Domain.SeedWork.Exceptions;

namespace camp_fire.Application.Services;

public class GameService : IGameService
{
    private readonly IUnitOfWork _unitOfWork;

    public GameService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<GameResponseVM>> GetAsync(GetGameRequestVM request)
    {
        var games = _unitOfWork.GetRepository<Game>().Find(x =>

        request.Ids == null || request.Ids.Contains(x.Id)
        && string.IsNullOrEmpty(request.Name) || x.Name.ToLower().Contains(request.Name.ToLower().Trim())
        && (request.Type == null || x.Type == request.Type))

        .Select(x => new GameResponseVM
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            Type = x.Type
        }).ToList();

        return await Task.FromResult(games);
    }

    public async Task<GameResponseVM?> GetByIdAsync(int id)
    {
        var game = await _unitOfWork.GetRepository<Game>().GetByIdAsync(id);

        if (game is null)
            throw new ApiException("story couldn't find ");

        var mappedGame = MapGameResposeVMHelper(game);

        return mappedGame;
    }

    public async Task<int> CreateAsync(CreateGameRequestVM request)
    {
        var game = new Game
        {
            Name = request.Name,
            Type = request.Type
        };

        await _unitOfWork.GetRepository<Game>().CreateAsync(game);

        return await _unitOfWork.SaveChangesAsync();
    }

    public async Task<GameResponseVM> UpdateAsync(UpdateGameRequestVM request)
    {
        var game = await _unitOfWork.GetRepository<Game>().GetByIdAsync(request.Id);

        if (game is null)
            throw new ApiException("Game couldn't find");
        game.Name = request.Name;
        game.Description = request.Description;
        game.Type = request.Type;

        _unitOfWork.GetRepository<Game>().Update(game);

        await _unitOfWork.SaveChangesAsync();

        var newGame = await GetByIdAsync(request.Id);

        return newGame!;
    }

    public async Task DeleteAsync(int id)
    {
        var game = await _unitOfWork.GetRepository<Game>().GetByIdAsync(id);

        if (game is null)
            throw new ApiException("game couldn't find");

        _unitOfWork.GetRepository<Game>().Delete(game);
    }

    #region Helpers 

    private GameResponseVM MapGameResposeVMHelper(Game game)
    {
        var gameResponseVM = new GameResponseVM
        {
            Id = game.Id,
            Name = game.Name,
            Description = game.Description,
            Type = game.Type
        };

        return gameResponseVM;
    }

    #endregion
}

