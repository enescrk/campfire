using camp_fire.Domain.Entities.Base;
using camp_fire.Domain.Enums;

namespace camp_fire.Domain.Entities;

public class Story : BaseEntity
{
    public string? Title { get; set; }
    public string? Text { get; set; }
    public StoryType Type { get; set; }
}
