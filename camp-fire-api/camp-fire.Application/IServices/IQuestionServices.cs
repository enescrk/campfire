using camp_fire.Application.Models;
using camp_fire.Domain.Entities;

namespace camp_fire.Application.IServices;

public interface IQuestionService
{
    Task<int> CreateAsync(CreateQuestionRequestVM request);
    Task<QuestionResponseVM> UpdateAsync(UpdateQuestionRequestVM request);
    Task<QuestionResponseVM?> GetByIdAsync(int id);
}

