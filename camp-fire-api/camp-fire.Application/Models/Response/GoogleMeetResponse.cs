// using Newtonsoft.Json;

// namespace camp_fire.Application.Models.Response;

// public partial class GoogleMeetResponse
// {
//     [JsonProperty("AnyoneCanAddSelf")]
//     public object AnyoneCanAddSelf { get; set; }

//     [JsonProperty("Attachments")]
//     public object Attachments { get; set; }

//     [JsonProperty("Attendees")]
//     public object Attendees { get; set; }

//     [JsonProperty("AttendeesOmitted")]
//     public object AttendeesOmitted { get; set; }

//     [JsonProperty("ColorId")]
//     public object ColorId { get; set; }

//     [JsonProperty("ConferenceData")]
//     public ConferenceData ConferenceData { get; set; }

//     [JsonProperty("CreatedRaw")]
//     public DateTimeOffset CreatedRaw { get; set; }

//     [JsonProperty("CreatedDateTimeOffset")]
//     public DateTimeOffset CreatedDateTimeOffset { get; set; }

//     [JsonProperty("Created")]
//     public DateTimeOffset Created { get; set; }

//     [JsonProperty("Creator")]
//     public Creator Creator { get; set; }

//     [JsonProperty("Description")]
//     public string Description { get; set; }

//     [JsonProperty("End")]
//     public End End { get; set; }

//     [JsonProperty("EndTimeUnspecified")]
//     public object EndTimeUnspecified { get; set; }

//     [JsonProperty("ETag")]
//     public string ETag { get; set; }

//     [JsonProperty("EventType")]
//     public string EventType { get; set; }

//     [JsonProperty("ExtendedProperties")]
//     public object ExtendedProperties { get; set; }

//     [JsonProperty("FocusTimeProperties")]
//     public object FocusTimeProperties { get; set; }

//     [JsonProperty("Gadget")]
//     public object Gadget { get; set; }

//     [JsonProperty("GuestsCanInviteOthers")]
//     public object GuestsCanInviteOthers { get; set; }

//     [JsonProperty("GuestsCanModify")]
//     public object GuestsCanModify { get; set; }

//     [JsonProperty("GuestsCanSeeOtherGuests")]
//     public bool GuestsCanSeeOtherGuests { get; set; }

//     [JsonProperty("HangoutLink")]
//     public Uri HangoutLink { get; set; }

//     [JsonProperty("HtmlLink")]
//     public Uri HtmlLink { get; set; }

//     [JsonProperty("ICalUID")]
//     public string ICalUid { get; set; }

//     [JsonProperty("Id")]
//     public string Id { get; set; }

//     [JsonProperty("Kind")]
//     public string Kind { get; set; }

//     [JsonProperty("Location")]
//     public string Location { get; set; }

//     [JsonProperty("Locked")]
//     public object Locked { get; set; }

//     [JsonProperty("Organizer")]
//     public Creator Organizer { get; set; }

//     [JsonProperty("OriginalStartTime")]
//     public object OriginalStartTime { get; set; }

//     [JsonProperty("OutOfOfficeProperties")]
//     public object OutOfOfficeProperties { get; set; }

//     [JsonProperty("PrivateCopy")]
//     public object PrivateCopy { get; set; }

//     [JsonProperty("Recurrence")]
//     public object Recurrence { get; set; }

//     [JsonProperty("RecurringEventId")]
//     public object RecurringEventId { get; set; }

//     [JsonProperty("Reminders")]
//     public Reminders Reminders { get; set; }

//     [JsonProperty("Sequence")]
//     public long Sequence { get; set; }

//     [JsonProperty("Source")]
//     public object Source { get; set; }

//     [JsonProperty("Start")]
//     public End Start { get; set; }

//     [JsonProperty("Status")]
//     public string Status { get; set; }

//     [JsonProperty("Summary")]
//     public string Summary { get; set; }

//     [JsonProperty("Transparency")]
//     public object Transparency { get; set; }

//     [JsonProperty("UpdatedRaw")]
//     public DateTimeOffset UpdatedRaw { get; set; }

//     [JsonProperty("UpdatedDateTimeOffset")]
//     public DateTimeOffset UpdatedDateTimeOffset { get; set; }

//     [JsonProperty("Updated")]
//     public DateTimeOffset Updated { get; set; }

//     [JsonProperty("Visibility")]
//     public object Visibility { get; set; }

//     [JsonProperty("WorkingLocationProperties")]
//     public object WorkingLocationProperties { get; set; }
// }

// public partial class ConferenceData
// {
//     [JsonProperty("ConferenceId")]
//     public string ConferenceId { get; set; }

//     [JsonProperty("ConferenceSolution")]
//     public ConferenceSolution ConferenceSolution { get; set; }

//     [JsonProperty("CreateRequest")]
//     public CreateRequest CreateRequest { get; set; }

//     [JsonProperty("EntryPoints")]
//     public List<EntryPoint> EntryPoints { get; set; }

//     [JsonProperty("Notes")]
//     public object Notes { get; set; }

//     [JsonProperty("Parameters")]
//     public object Parameters { get; set; }

//     [JsonProperty("Signature")]
//     public object Signature { get; set; }

//     [JsonProperty("ETag")]
//     public object ETag { get; set; }
// }

// public partial class ConferenceSolution
// {
//     [JsonProperty("IconUri")]
//     public Uri IconUri { get; set; }

//     [JsonProperty("Key")]
//     public Key Key { get; set; }

//     [JsonProperty("Name")]
//     public string Name { get; set; }

//     [JsonProperty("ETag")]
//     public object ETag { get; set; }
// }

// public partial class Key
// {
//     [JsonProperty("Type")]
//     public string Type { get; set; }

//     [JsonProperty("ETag")]
//     public object ETag { get; set; }
// }

// public partial class CreateRequest
// {
//     [JsonProperty("ConferenceSolutionKey")]
//     public Key ConferenceSolutionKey { get; set; }

//     [JsonProperty("RequestId")]
//     public string RequestId { get; set; }

//     [JsonProperty("Status")]
//     public Status Status { get; set; }

//     [JsonProperty("ETag")]
//     public object ETag { get; set; }
// }

// public partial class Status
// {
//     [JsonProperty("StatusCode")]
//     public string StatusCode { get; set; }

//     [JsonProperty("ETag")]
//     public object ETag { get; set; }
// }

// public partial class EntryPoint
// {
//     [JsonProperty("AccessCode")]
//     public object AccessCode { get; set; }

//     [JsonProperty("EntryPointFeatures")]
//     public object EntryPointFeatures { get; set; }

//     [JsonProperty("EntryPointType")]
//     public string EntryPointType { get; set; }

//     [JsonProperty("Label")]
//     public string Label { get; set; }

//     [JsonProperty("MeetingCode")]
//     public object MeetingCode { get; set; }

//     [JsonProperty("Passcode")]
//     public object Passcode { get; set; }

//     [JsonProperty("Password")]
//     public object Password { get; set; }

//     [JsonProperty("Pin")]
//     public object Pin { get; set; }

//     [JsonProperty("RegionCode")]
//     public object RegionCode { get; set; }

//     [JsonProperty("Uri")]
//     public Uri Uri { get; set; }

//     [JsonProperty("ETag")]
//     public object ETag { get; set; }
// }

// public partial class Creator
// {
//     [JsonProperty("DisplayName")]
//     public object DisplayName { get; set; }

//     [JsonProperty("Email")]
//     public string Email { get; set; }

//     [JsonProperty("Id")]
//     public object Id { get; set; }

//     [JsonProperty("Self")]
//     public bool Self { get; set; }
// }

// public partial class End
// {
//     [JsonProperty("Date")]
//     public object Date { get; set; }

//     [JsonProperty("DateTimeRaw")]
//     public DateTimeOffset DateTimeRaw { get; set; }

//     [JsonProperty("DateTimeDateTimeOffset")]
//     public DateTimeOffset DateTimeDateTimeOffset { get; set; }

//     [JsonProperty("DateTime")]
//     public DateTimeOffset DateTime { get; set; }

//     [JsonProperty("TimeZone")]
//     public string TimeZone { get; set; }

//     [JsonProperty("ETag")]
//     public object ETag { get; set; }
// }

// public partial class Reminders
// {
//     [JsonProperty("Overrides")]
//     public List<Override> Overrides { get; set; }

//     [JsonProperty("UseDefault")]
//     public bool UseDefault { get; set; }
// }

// public partial class Override
// {
//     [JsonProperty("Method")]
//     public string Method { get; set; }

//     [JsonProperty("Minutes")]
//     public long Minutes { get; set; }

//     [JsonProperty("ETag")]
//     public object ETag { get; set; }
// }

