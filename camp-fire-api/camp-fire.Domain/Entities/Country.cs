using camp_fire.Domain.Entities.Base;

namespace camp_fire.Domain.Entities;

public class Country : BaseEntity
{
    public string? Name { get; set; }

    public virtual ICollection<Address> Address { get; set; }


}
