using camp_fire.Domain.Entities.Base;

namespace camp_fire.Domain.Entities;

public class Page : BaseEntity
{
    public string? Name { get; set; }
    public int EventId { get; set; }
    public int ScoreboardId { get; set; }
    public bool IsCompleted { get; set; } //oyun tamamlandı mı?

    public virtual Event? Event { get; set; }
    public virtual Scoreboard? Scoreboard { get; set; }
}
