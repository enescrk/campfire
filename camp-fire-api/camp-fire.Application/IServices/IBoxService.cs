using camp_fire.Application.Models;

namespace camp_fire.Application.IServices;

public interface IBoxService
{
    Task<int> CreateAsync(CreateBoxRequestVM request);
    Task<BoxResponseVM> UpdateAsync(UpdateBoxRequestVM request);
    Task<BoxResponseVM?> GetByIdAsync(int id);
    Task<List<BoxResponseVM>> GetAsync(GetBoxRequestVM request);
}
