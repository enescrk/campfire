using camp_fire.Domain.Entities.Base;

namespace camp_fire.Domain.Entities;

public class Box : BaseEntity
{
    public List<string> Images { get; set; }
    public string Description { get; set; }
}
