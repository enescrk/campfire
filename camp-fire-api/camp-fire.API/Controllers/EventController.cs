using camp_fire.API.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace camp_fire.API.Controllers;

[ApiController]
[Route("[controller]")]
public class EventController : ControllerBase
{
    private readonly ILogger<EventController> _logger;
    private readonly IHubContext<EventHub> _hub;

    public EventController(ILogger<EventController> logger,
                            IHubContext<EventHub> hub)
    {
        _logger = logger;
        _hub = hub;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        await _hub.Clients.All.SendAsync("hubData", Foo());

        return Ok();
    }

    private string Foo()
    {
        return "test";
    }
}
