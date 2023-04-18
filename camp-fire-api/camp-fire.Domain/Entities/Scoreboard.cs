using camp_fire.Domain.Entities.Base;

namespace camp_fire.Domain.Entities;

public class Scoreboard : BaseEntity
{
    public int EventId { get; set; }
    public int PageId { get; set; }
    public int UserId { get; set; }
    public int Score { get; set; }
}