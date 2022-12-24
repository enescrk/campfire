/* using camp_fire.Domain.Entities.Base;
using camp_fire.Domain.Enums;

namespace camp_fire.Domain.Entities;

public class Question : BaseEntity
{
    public string? Text { get; set; }
    public QuestionType Type { get; set; }
}
*/
using camp_fire.Domain.Entities;
using camp_fire.Domain.Enums;

namespace camp_fire.Application.Models;

public class QuestionResponseVM
{
    public string Text { get; set; }
    public QuestionType Type { get; set; }
}
  


