using camp_fire.Domain.Entities.Base;

namespace camp_fire.Domain.Entities;

public class EnterpriceLevel : BaseEntity
{
    public decimal Price { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}