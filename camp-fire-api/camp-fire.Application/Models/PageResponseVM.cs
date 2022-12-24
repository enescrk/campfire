//public class Page : BaseEntity
//{
//    public string? Name { get; set; }
//   public int EventId { get; set; }

//   public virtual Event? Event { get; set; }
//   public virtual Scoreboard? Scoreboard { get; set; }
//}
using camp_fire.Domain.Entities;

namespace camp_fire.Application.Models;

public class PageResponseVM
{
    public int EventId { get; set; }
    public string Name { get; set; }
    public Scoreboard Scoreboard { get; set; }
    public Event Event { get; set; }
}
