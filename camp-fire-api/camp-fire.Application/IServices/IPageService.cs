using camp_fire.Application.Models;
using camp_fire.Domain.Entities;

namespace camp_fire.Application.IServices;

public interface IPageService
{
    Task<int> CreateAsync(Page request);
    Task<Page> UpdateAsync(Page request);
    Task<PageResponseVM?> GetAsync(int id);
}
