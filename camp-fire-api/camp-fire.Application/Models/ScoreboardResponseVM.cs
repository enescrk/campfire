/*
using camp_fire.Domain.Entities.Base;

namespace camp_fire.Domain.Entities;

public class Scoreboard : BaseEntity
{
    public int EventId { get; set; }
    public int PageId { get; set; }
    public int UserId { get; set; }
    public int Score { get; set; }

    public virtual Event Event { get; set; }
    public virtual Page Page { get; set; }
    public virtual User User { get; set; }



}

*/
using camp_fire.Domain.Entities;

namespace camp_fire.Application.Models;

public class ScoreboardResponseVM
{
    public int Id { get; set; }
    public int EventId { get; set; }
    public int PageId { get; set; }
    public int UserId { get; set; }
    public int Score { get; set; }
    // public User User { get; set; }
    // public Scoreboard Scoreboard { get; set; }
    // public Page Page { get; set; }
}

public class UpdateScoreboardRequestVM : ScoreboardResponseVM
{
}
