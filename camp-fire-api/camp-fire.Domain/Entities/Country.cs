using camp_fire.Domain.Entities.Base;

namespace camp_fire.Domain.Entities;

public class Country : BaseEntity
{
    public Country()
    {
        Addresses = new List<Address>();
    }

    public string? Name { get; set; }

    public virtual ICollection<Address> Addresses { get; set; }
}
