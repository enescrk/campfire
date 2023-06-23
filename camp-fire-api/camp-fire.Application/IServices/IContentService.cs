using camp_fire.Application.Models.Request;
using camp_fire.Domain.Entities;

namespace camp_fire.Application.IServices;

public interface IContentService
{
    Task<int> CreateAsync(Content request);
    Task<Content> UpdateAsync(Content request);
    Task<Content?> GetByIdAsync(int id);
    Task<List<Content>> GetAsync(GetContentRequestVM request);
    Task DeleteAsync(int id);
}