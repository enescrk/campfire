using camp_fire.Application.Models;
using camp_fire.Domain.Entities;

namespace camp_fire.Application.IServices;

public interface IQuestionService
{
    Task<int> CreateAsync(Question request);
    Task<Event> UpdateAsync(Question request);
    Task<QuestionResponseVM?> GetAsync(int id);
}
