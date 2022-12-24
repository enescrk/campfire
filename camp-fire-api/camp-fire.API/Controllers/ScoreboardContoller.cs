using camp_fire.API.Configurations;
using camp_fire.API.Hubs;
using camp_fire.API.Models;
using camp_fire.Application.IServices;
using camp_fire.Application.Models;
using camp_fire.Application.Services;
using camp_fire.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace camp_fire.API.Controllers;

[Route("[controller]")]
public class ScoreboardController : BaseApiController
{
    private readonly ILogger<ScoreboardController> _logger;
    private readonly IScoreboardService _scoreboardService;
    private readonly EventService _eventService;
    private readonly IHubContext<EventHub> _eventHub;

    public ScoreboardController(ILogger<ScoreboardController> logger,
                            IScoreboardService scoreboardService,
                            EventService eventService,
                            IHubContext<EventHub> eventHub
                            ) : base(logger)
    {
        _logger = logger;
        _scoreboardService = scoreboardService;
        _eventService = eventService;
        _eventHub = eventHub;
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _scoreboardService.GetAsync(id);
        return Ok(new BaseApiResult { Data = result });
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Post([FromBody] Scoreboard request)
    {
        var result = await _scoreboardService.CreateAsync(request);
        return Ok(result);
    }

    [HttpPut]
    [AllowAnonymous]
    public async Task<IActionResult> Put([FromBody] UpdateScoreboardRequestVM request)
    {
        var result = await _scoreboardService.UpdateAsync(request);

        var eventt = await _eventService.GetAsync(result.EventId);

        if (eventt is not null)
        {
            var eventHubModel = MapEventHubModelHelper(eventt);

            await _eventHub.Clients.All.SendAsync("GetEvent", eventHubModel);
        }

        return Ok(result);
    }

    [HttpGet("test")]
    [AllowAnonymous]
    public async Task<IActionResult> Get([FromBody] Scoreboard request)
    {
        // var result = await _eventService.UpdateAsync(request);

        return Ok();
    }

    #region Helpers

    private EventHubResponseVM MapEventHubModelHelper(EventResponseVM eventt)
    {
        var eventHubModel = new EventHubResponseVM
        {
            Id = eventt.Id,
            CurrentPageId = 0, //* TODO: Tabloya eklenip dinamikleştirilecek.
            Date = eventt.Date,
            Name = eventt.Name,
            PageIds = eventt.PageIds,
            ParticipiantIds = eventt.ParticipiantIds?.ToList(),
            Scoreboards = eventt.Scoreboards?.Select(x => new ScoreboardResponseVM
            {
                Id = x.Id,
                EventId = x.EventId,
                PageId = x.PageId,
                UserId = x.UserId,
                Score = x.Score
            }).ToList()
        };

        return eventHubModel;
    }

    #endregion
}
