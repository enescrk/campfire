using camp_fire.Domain.Entities;

namespace camp_fire.Application.Models;

public class EventResponseVM
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? HashedKey { get; set; }
    public DateTime Date { get; set; }
    public UserResponseVM? User { get; set; }
    public List<ScoreboardResponseVM>? Scoreboards { get; set; }
    public List<int>? PageIds { get; set; }
    public List<int>? ParticipiantIds { get; set; }
}

public class UpdateEventRequestVM
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? HashedKey { get; set; }
    public DateTime Date { get; set; }
    public int[]? ParticipiantIds { get; set; }
    public int? CompanyId { get; set; }
    public int[]? PageIds { get; set; }
    public string? MeetingUrl { get; set; }
}
