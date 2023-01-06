using camp_fire.Application.Models;

namespace camp_fire.API.Models;

public class EventHubResponseVM
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public DateTime Date { get; set; }
    public List<int>? PageIds { get; set; }
    public int CurrentPageId { get; set; }
    public List<int>? ParticipiantIds { get; set; }
    public List<UserResponseVM>? ParticipiantUsers { get; set; }
    public List<ScoreboardResponseVM>? Scoreboards { get; set; }
}
