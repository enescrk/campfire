using camp_fire.Application.Models;
using camp_fire.Domain.Entities;

namespace camp_fire.Application.IServices;

public interface IPageService
{
    Task<int> CreateAsync(Page request);
    Task<Page> UpdateAsync(PageResponseVM request);
    Task<List<PageResponseVM>> GetAsync(GetPagesRequestVM request);
    Task<PageResponseVM?> GetByIdAsync(int id);
    Task UpdateIsCompleteAsync(UpdatePageIsCompleteRequestVM request);
}
