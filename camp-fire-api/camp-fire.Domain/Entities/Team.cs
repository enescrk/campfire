using camp_fire.Domain.Entities.Base;

namespace camp_fire.Domain.Entities;

public class Team : BaseEntity
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public List<int>? UserIds { get; set; }
    public List<int>? BookingIds { get; set; }
}
