//public class Page : BaseEntity
//{
//    public string? Name { get; set; }
//   public int EventId { get; set; }

//   public virtual Event? Event { get; set; }
//   public virtual Scoreboard? Scoreboard { get; set; }
//}

namespace camp_fire.Application.Models;

public class PageResponseVM
{
    public int Id { get; set; }
    public int? EventId { get; set; }
    public string? Name { get; set; }
    public string? EventName { get; set; }
    public int? ScoreboardId { get; set; }
    public bool IsCompleted { get; set; }
}

public class UpdatePageIsCompleteRequestVM
{
    public int Id { get; set; }
    public bool IsCompleted { get; set; }
}

public class GetPagesRequestVM
{
    public int? Id { get; set; }
    public int? EventId { get; set; }
    public string? Name { get; set; }
    public string? EventName { get; set; }
    public int? ScoreboardId { get; set; }
}
