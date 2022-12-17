using camp_fire.Domain.Entities.Base;

namespace camp_fire.Domain.Entities;

public class Event : BaseEntity
{
    public string? Name { get; set; }

    public DateTime Date { get; set; }
    public int[]? ParticipiantIds { get; set; }
    public int? CompanyId { get; set; }
    public int[]? PageIds { get; set; }
    public string? HashedKey { get; set; }
    public string? MeetingUrl { get; set; }
    public int UserId { get; set; }
    
    
    

    public virtual User? User { get; set; }
    public virtual ICollection<Page> Page { get; set; }
    public virtual ICollection<Scoreboard> Scoreboard { get; set; }
    


}
