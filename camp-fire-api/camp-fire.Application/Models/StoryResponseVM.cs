/*using camp_fire.Domain.Entities.Base;
using camp_fire.Domain.Enums;

namespace camp_fire.Domain.Entities;

public class Story : BaseEntity
{
    public string? Text { get; set; }
    public StoryType Type { get; set; }
}

*/
using camp_fire.Domain.Entities;

namespace camp_fire.Application.Models;

public class StoryResponseVM
{
    public string Text { get; set; }
    public StoryType Type { get; set; }
}
