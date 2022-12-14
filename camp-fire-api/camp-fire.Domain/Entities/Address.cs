using camp_fire.Domain.Entities.Base;

namespace camp_fire.Domain.Entities;

public class Address : BaseEntity
{
    public int CountryId { get; set; }
    public string? City { get; set; }
    public string? County { get; set; }
    public string? OpenAddress { get; set; }
    public int UserId { get; set; }

    public virtual User User { get; set; }
    public virtual Country Country { get; set; }
}
