using camp_fire.Domain.Entities;
using camp_fire.Domain.SeedWork;
using Microsoft.AspNetCore.SignalR;

namespace camp_fire.API.Hubs;

public class EventHub : Hub
{
    private readonly IUnitOfWork _unitOfWork;

    public EventHub(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task SendMessage(string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", message);
    }

    public async Task SendEvent(string id)
    {
        var eventt = await _unitOfWork.GetRepository<Event>().GetByIdAsync(Convert.ToInt32(id));

        await Clients.All.SendAsync("GetEvent", eventt.Name);
    }
}