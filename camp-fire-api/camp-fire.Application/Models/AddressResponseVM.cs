namespace camp_fire.Application.Models;

public class AddressResponseVM
{
    public int CountryId { get; set; }
    public int UserId { get; set; }
    public string? City { get; set; }
    public string? County { get; set; }
    public string? OpenAddress { get; set; }

}
public class GetAddressRequestVM
{
    public int? Id { get; set; }
    public int? CountryId { get; set; }
    public int? UserId { get; set; }
    public string? City { get; set; }
    public string? County { get; set; }
    public string? OpenAddress { get; set; }
}

public class UpdateAddressRequestVM : CreateAddressRequestVM
{
    public int Id { get; set; }
}

public class CreateAddressRequestVM
{
    public int CountryId { get; set; }
    public int UserId { get; set; }
    public string? City { get; set; }
    public string? County { get; set; }
    public string? OpenAddress { get; set; }
}