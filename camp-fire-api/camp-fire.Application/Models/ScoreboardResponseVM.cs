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
