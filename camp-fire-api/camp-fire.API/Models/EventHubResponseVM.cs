using camp_fire.Application.Models;

namespace camp_fire.API.Models;

public class EventHubResponseVM
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public DateTime Date { get; set; }
    public List<int>? PageIds { get; set; } // oyun listesi
    public int CurrentPageId { get; set; } // şu anki oyun
    public int CurrentUserId { get; set; } // sıra kimde
    public List<int>? ParticipiantIds { get; set; }
    public List<UserResponseVM>? ParticipiantUsers { get; set; }
    // public List<ActiveUserResponseVM>? AvtiveUsers { get; set; }
    public List<ScoreboardResponseVM>? Scoreboards { get; set; }
}
