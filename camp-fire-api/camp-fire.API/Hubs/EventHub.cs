// using camp_fire.API.IHubs;
using camp_fire.API.Models;
using camp_fire.Domain.Entities;
using camp_fire.Domain.SeedWork;
using Microsoft.AspNetCore.SignalR;

namespace camp_fire.API.Hubs;

public class EventHub : Hub
{
    // private readonly IUnitOfWork _unitOfWork;

    // public EventHub(IUnitOfWork unitOfWork)
    // {
    //     _unitOfWork = unitOfWork;
    // }

    public async Task SendMessage(string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", message);
    }

    public async Task SendEvent(EventHubResponseVM eventt)
    {
        await Clients.All.SendAsync("GetEvent", eventt);
    }
}