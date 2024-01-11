using System.Text.Json.Serialization;

namespace camp_fire.Application.Models.Request;

public class CreateGoogleCalendarEventVM
{
    public required DateTime StartDate { get; set; }
    [JsonIgnore]
    public required DateTime EndDate { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public List<string> Attendees { get; set; }
}