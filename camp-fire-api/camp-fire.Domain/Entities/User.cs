using camp_fire.Domain.Entities.Base;
using camp_fire.Domain.Enums;

namespace camp_fire.Domain.Entities;

public class User : BaseEntity
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public int[]? AuthorizedCompanies { get; set; }
    public bool Gender { get; set; } //* male=true female=false
    public int? AddressId { get; set; }
    public string? EMail { get; set; }
    public UserType UserType { get; set; }
    public string? PhoneNumber { get; set; }
    public bool IsActive { get; set; }
    // public int? CompanyId { get; set; }
    // public string CompanyName { get; set; }
    // public int? TeamId { get; set; }
    // public string TeamName { get; set; }
    // public DateTime? BirthDate { get; set; }
    // public string Image { get; set; }
    // public int? SubscribtionId { get; set; }

    // public virtual ICollection<Event>? Events { get; set; }
    // public virtual ICollection<UserConfirmation>? UserConfirmations { get; set; }
    // public virtual Address? Address { get; set; }
    // public virtual ICollection<EventParticipant> ParticipatedEvents { get; set; }
}
