using camp_fire.Domain.Entities.Base;
using camp_fire.Domain.Enums;

namespace camp_fire.Domain.Entities;

public class Address : BaseEntity
{
    public string Title { get; set; }
    public int CountryId { get; set; }
    public string? City { get; set; }
    public string? County { get; set; }
    public string? District { get; set; }
    public string? Street { get; set; }
    public string? BuildNo { get; set; }
    public string? DoorNo { get; set; }
    public string? ZipCode { get; set; }
    public string? OpenAddress { get; set; }
    public int UserId { get; set; }
    public int? CompanyId { get; set; }
    public string? TaxNumber { get; set; }
    public string? TaxAdminstration { get; set; }
    public AddressType? Type { get; set; }

    // public virtual User User { get; set; }
    // public virtual Country Country { get; set; }
}