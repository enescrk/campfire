namespace camp_fire.Application.Models.Request;

public class GetEventsRequestVM
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public DateTime? Date { get; set; }
    public int? CompanyId { get; set; }
    public int? UserId { get; set; }
    public int? ParticipiantId { get; set; }
    public string? HashedKey { get; set; }
    public string? MeetingUrl { get; set; }
    public int? CurrentPageId { get; set; }
    public int? CurrentUserId { get; set; }
}