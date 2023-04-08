using camp_fire.Application.Models.Request;
using camp_fire.Application.Models.Response;

namespace camp_fire.Application.IServices;

public interface IGameService
{
    Task<List<GameResponseVM>> GetAsync(GetGameRequestVM request);
    Task<int> CreateAsync(CreateGameRequestVM request);
    Task<GameResponseVM> UpdateAsync(UpdateGameRequestVM request);
    Task<GameResponseVM?> GetByIdAsync(int id);
    Task DeleteAsync(int id);
}
