namespace camp_fire.Domain.Entities;

public class Event
{
    public string? Name { get; set; }
    public DateTime Date { get; set; }
    public int[]? ParticipiantIds { get; set; }
    public int? CompanyId { get; set; }
    public int[]? PageIds { get; set; }
    public string? HashedKey { get; set; }
    public string? MeetingUrl { get; set; }
}
