using camp_fire.Domain.Enums;

namespace camp_fire.Application.Models;

public class StoryResponseVM
{
    public int? Id { get; set; }
    public string? Title { get; set; }
    public string? Text { get; set; }
    public StoryType Type { get; set; }
}

public class GetStorysRequestVM
{
    public int? Id { get; set; }
    public string? Text { get; set; }
    public StoryType? Type { get; set; }
}

public class UpdateStoryRequestVM : StoryResponseVM
{
}

public class CreateStoryRequestVM : StoryResponseVM
{
}