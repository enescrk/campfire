namespace camp_fire.Application.Models;

public class CreateBookingRequest
{
    public int ExperienceId { get; set; }
    public List<string> ParticipantMails { get; set; }
    public int? CompanyId { get; set; }
    public int? ModeratorId { get; set; }
    public DateTime Date { get; set; }
    public int? OwnerId { get; set; }
    public string RecordUrl { get; set; }
    public string MeetingUrl { get; set; }
}