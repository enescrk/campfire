namespace camp_fire.Application.Models.Response;

public class BookingResponse
{
    public int? Id { get; set; }
    public int ExperienceId { get; set; }
    public string ExperienceTitle { get; set; }
    public DateTime Date { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public List<string> ParticipantUsers { get; set; }
}