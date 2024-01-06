using System.Text.Json.Serialization;

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
    [JsonIgnore]
    public int CreatedBy { get; set; }
}

public class UpdateBookingRequest
{
    public int Id { get; set; }
    public List<string> ParticipantMails { get; set; }
    public int? CompanyId { get; set; }
    public int? ModeratorId { get; set; }
    public DateTime Date { get; set; }
    public int? OwnerId { get; set; }
    public string MeetingUrl { get; set; }
    [JsonIgnore]
    public int CreatedBy { get; set; }
}

public class GetBookingsRequest
{
    public int? Id { get; set; }
    public int? ExperienceId { get; set; }
    public List<int>? UserIds { get; set; }
    public int? CompanyId { get; set; }
    public int? ModeratorId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int? OwnerId { get; set; }
    [JsonIgnore]
    public int CreatedBy { get; set; }
}