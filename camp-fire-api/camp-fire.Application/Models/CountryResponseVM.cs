//using camp_fire.Domain.Entities.Base;

//namespace camp_fire.Domain.Entities;

//public class Country : BaseEntity
//{
//  public Country()
// {
   //    Addresses = new List<Address>();
   // }

    //public string? Name { get; set; }

   // public virtual ICollection<Address> Addresses { get; set; }
//}
//
using camp_fire.Domain.Entities;

namespace camp_fire.Application.Models;

public class CountryResponseVM
{
    public string Name { get; set; }
    public User UserName { get; set; }
    public List<Address> Addresses { get; set; }
}
