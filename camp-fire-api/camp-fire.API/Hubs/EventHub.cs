using Microsoft.AspNetCore.SignalR;

namespace camp_fire.API.Hubs;

public class EventHub : Hub
{
    public async Task SendMessage(string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", message);
    }
}