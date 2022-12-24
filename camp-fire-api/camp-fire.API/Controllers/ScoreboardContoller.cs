using camp_fire.API.Configurations;
using camp_fire.Application.IServices;
using camp_fire.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace camp_fire.API.Controllers;

[Route("[controller]")]
public class ScoreboardController : BaseApiController
{
    private readonly ILogger<ScoreboardController> _logger;
    private readonly IScoreboardService _scoreboardService;

    public ScoreboardController(ILogger<ScoreboardController> logger,
                            IScoreboardService scoreboardService
                            ) : base(logger)
    {
        _logger = logger;
        _scoreboardService = scoreboardService;
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
    public async Task<IActionResult> Put([FromBody] Scoreboard request)
    {
        var result = await _scoreboardService.UpdateAsync(request);

        return Ok(result);
    }

    [HttpGet("test")]
    [AllowAnonymous]
    public async Task<IActionResult> Get([FromBody] Scoreboard request)
    {
        // var result = await _eventService.UpdateAsync(request);

        return Ok();
    }
}
