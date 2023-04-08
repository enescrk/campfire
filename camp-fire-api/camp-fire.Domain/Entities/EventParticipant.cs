using camp_fire.Domain.Entities;
using camp_fire.Domain.Entities.Base;

public class EventParticipant : BaseEntity
{
    public int UserId { get; set; }
    public int EventId { get; set; }
    public virtual User User { get; set; }
    public virtual Event Event { get; set; }
}