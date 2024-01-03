using System.Net.Mail;
using camp_fire.Application.IServices;
using camp_fire.Application.Models.Request;
using camp_fire.Infrastructure.Helpers;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Microsoft.Extensions.Options;

namespace camp_fire.Application.Services;

public class GoogleCalendarEventService : IGoogleCalendarEventService
{
    private readonly GoogleCalendarApiSettings _googleCalendarApiSettings;

    public GoogleCalendarEventService(IOptions<GoogleCalendarApiSettings> googleCalendarApiSettings)
    {
        _googleCalendarApiSettings = googleCalendarApiSettings.Value;
    }

    public async Task<Event?> CreateEventAsync(CreateGoogleCalendarEventVM request)
    {
        var service = await GetCalendarServiceAsync();

        var attendees = new List<EventAttendee>();

        foreach (var email in request.Attendees)
        {
            var attendee = new EventAttendee
            {
                Email = email
            };

            attendees.Add(attendee);
        }

        var newEvent = new Event()
        {
            Summary = request.Title,
            Location = "Online",
            Description = request.Description,
            Start = new EventDateTime
            {
                DateTime = request.StartDate,
                TimeZone = "Europe/Istanbul" // İstanbul zaman dilimi
            },
            End = new EventDateTime
            {
                DateTime = request.EndDate,
                TimeZone = "Europe/Istanbul"
            },
            Reminders = new Event.RemindersData
            {
                UseDefault = false,
                Overrides = new List<EventReminder>
                {
                    new EventReminder { Method = "popup", Minutes = 10 },
                },
            },
            GuestsCanSeeOtherGuests = true,
            Attendees = attendees,
            Creator = new Event.CreatorData
            {
                DisplayName = "Experience Hub",
                Email = "experiences.teams@gmail.com",
            },
            ConferenceData = new ConferenceData
            {
                CreateRequest = new CreateConferenceRequest
                {
                    RequestId = $"{request.Title}_{request.StartDate}_{request.EndDate}",
                    ConferenceSolutionKey = new ConferenceSolutionKey
                    {
                        Type = "hangoutsMeet",
                    },
                },
            },
        };

        var serviceRequest = service.Events.Insert(newEvent, _googleCalendarApiSettings.CalendarId);
        serviceRequest.ConferenceDataVersion = 1;
        var createdEvent = await serviceRequest.ExecuteAsync();

        Console.WriteLine($"Etkinlik oluşturuldu: {createdEvent.HtmlLink}");

        return await GetMeetLinkAsync(service, _googleCalendarApiSettings.CalendarId, createdEvent.Id);
    }

    private async Task<Event?> GetMeetLinkAsync(CalendarService service, string calendarId, string eventId)
    {
        var getRequest = service.Events.Get(calendarId, eventId);
        var eventDetails = await getRequest.ExecuteAsync();
        Console.WriteLine($"Etkinlik detayları:{eventDetails.ToJson()}");

        return eventDetails;
    }

    private async Task<CalendarService> GetCalendarServiceAsync()
    {
        var credential = await GetUserCredentialAsync();

        var service = new CalendarService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = _googleCalendarApiSettings.ApplicationName,
        });

        return service;
    }

    private async Task<UserCredential> GetUserCredentialAsync()
    {
        using (var stream = new FileStream(_googleCalendarApiSettings.CredentialsPath, FileMode.Open, FileAccess.Read))
        {
            var credPathToken = _googleCalendarApiSettings.TokenPath;
            return await GoogleWebAuthorizationBroker.AuthorizeAsync(
                GoogleClientSecrets.Load(stream).Secrets,
                new[] { CalendarService.Scope.Calendar },
                "user",
                CancellationToken.None,
                new FileDataStore(credPathToken, true));
        }
    }
}

public class GoogleCalendarApiSettings
{
    public string CredentialsPath { get; set; }
    public string TokenPath { get; set; }
    public string CalendarId { get; set; }
    public string ApplicationName { get; set; }
}