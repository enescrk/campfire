namespace camp_fire.Application.Models;

public class ScoreboardResponseVM
{
    public int Id { get; set; }
    public int EventId { get; set; }
    public int PageId { get; set; }
    public int UserId { get; set; }
    public int Score { get; set; }
}
public class ScoreboardsByEventIdResponseVM
{
    public int PageId { get; set; }
    public int UserId { get; set; }
    public int TotalScore { get; set; }
}

public class UpdateScoreboardRequestVM : ScoreboardResponseVM
{
}


public class CreateScoreboardRequestVM : ScoreboardResponseVM
{
}

public class GetScoreboardRequestVM
{
    public int EventId { get; set; }
}