using camp_fire.Domain.Entities;
namespace camp_fire.Application.Models;

public class CountryResponseVM
{
    public string? Name { get; set; }
    public User? UserName { get; set; }
    public List<Address> Addresses { get; set; }
}
