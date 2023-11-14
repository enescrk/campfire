using camp_fire.Domain.Entities.Base;

namespace camp_fire.Domain.Entities;

public class Agenda : BaseEntity
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public int? Duration { get; set; }
}
