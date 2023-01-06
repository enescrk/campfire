using camp_fire.Domain.Entities;
using camp_fire.Domain.Enums;

namespace camp_fire.Application.Models;

public class QuestionResponseVM
{
    public int? Id { get; set; }
    public string? Text { get; set; }
    public QuestionType Type { get; set; }
}
public class GetQuestionsRequestVM
{
    public int? Id { get; set; }
    public string? Text { get; set; }
    public QuestionType? Type { get; set; }
}
public class UpdateQuestionRequestVM : QuestionResponseVM
{
    
}

public class CreateQuestionRequestVM : QuestionResponseVM
{
}




