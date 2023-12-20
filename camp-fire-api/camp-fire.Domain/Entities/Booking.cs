using camp_fire.Domain.Entities.Base;

public class Booking : BaseEntity
{
    public int ExperienceId { get; set; }
    public List<int> UserIds { get; set; }
    public int? CompanyId { get; set; }
    public int? ModeratorId { get; set; }
    public DateTime Date { get; set; }
    public int? OwnerId { get; set; }
    public string RecordUrl { get; set; }
    public string MeetingUrl { get; set; }
}