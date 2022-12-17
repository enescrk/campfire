using camp_fire.Domain.Entities.Base;
using camp_fire.Domain.Enums;

namespace camp_fire.Domain.Entities;

public class User : BaseEntity
{
    public User()
    {
        UserConfirmations = new List<UserConfirmation>();
        Events = new List<Event>();
    }

    public string? Name { get; set; }
    public string? Surname { get; set; }
    public int[]? AuthorizedCompanies { get; set; }
    public bool Gender { get; set; } //* male=true female=false
    public int? AddressId { get; set; }
    public string? EMail { get; set; }
    public UserType UserType { get; set; }
    public string? PhoneNumber { get; set; }

    public virtual ICollection<Event>? Events { get; set; }
    public virtual ICollection<UserConfirmation>? UserConfirmations { get; set; }
    public virtual Address? Addresses { get; set; }
}
