using camp_fire.Application.IServices;
using camp_fire.Application.Models;
using camp_fire.Domain.Entities;
using camp_fire.Domain.SeedWork;
using camp_fire.Domain.SeedWork.Exceptions;

namespace camp_fire.Application.Services;

public class QuestionService : IQuestionService
{
    private readonly IUnitOfWork _unitOfWork;

    public QuestionService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<List<QuestionResponseVM>> GetAsync(GetQuestionsRequestVM request)
    {
        var questions = _unitOfWork.GetRepository<Question>().Find(x =>

        request.Id == null || x.Id == request.Id
        && (request.Type == null || x.Type == request.Type))

        .Select(x => new QuestionResponseVM
        {
            Id = x.Id,
            Text = x.Text,
            Type = x.Type
        }).ToList();

        return await Task.FromResult(questions);
    }
    public async Task<QuestionResponseVM?> GetByIdAsync(int id)
    {
        var question = await _unitOfWork.GetRepository<Question>().GetByIdAsync(id);

        if (question is null)
            throw new ApiException("Question couldn't find ");

        var mappedQuestion = MapQuestionResposeVMHelper(question);

        return mappedQuestion;
    }

    public QuestionResponseVM? GetByRandomAsync()
    {
        var questions = _unitOfWork.GetRepository<Question>().GetAll();

        Random rand = new Random();
        int toSkip = rand.Next(0, questions.Count());

        var question = questions.Skip(toSkip).Take(1).First();

        var mappedQuestion = MapQuestionResposeVMHelper(question!);

        return mappedQuestion;
    }

    public async Task<int> CreateAsync(CreateQuestionRequestVM request)
    {
        var question = new Question
        {
            Text = request.Text,
            Type = request.Type
        };

        await _unitOfWork.GetRepository<Question>().CreateAsync(question);

        return await _unitOfWork.SaveChangesAsync();
    }

    public async Task<QuestionResponseVM> UpdateAsync(UpdateQuestionRequestVM request)
    {
        var question = await _unitOfWork.GetRepository<Question>().GetByIdAsync(request.Id!.Value);

        if (question is null)
            throw new ApiException("Question couldn't find");
        question.Text = request.Text;
        question.Type = request.Type;

        _unitOfWork.GetRepository<Question>().Update(question);

        await _unitOfWork.SaveChangesAsync();

        var newQuestion = await _unitOfWork.GetRepository<Question>().GetByIdAsync(request.Id!.Value);

        var mappedQuestion = MapQuestionResposeVMHelper(newQuestion);

        return mappedQuestion;
    }

    public async Task DeleteAsync(int id)
    {
        var question = await _unitOfWork.GetRepository<Question>().GetByIdAsync(id);

        if (question is null)
            throw new ApiException("Question couldn't find");

        _unitOfWork.GetRepository<Question>().Delete(question);
    }

    #region Helpers 

    private QuestionResponseVM MapQuestionResposeVMHelper(Question Question)
    {
        var QuestionResponseVM = new QuestionResponseVM
        {
            Text = Question.Text,
            Type = Question.Type
        };
        return QuestionResponseVM;
    }

    #endregion
}



