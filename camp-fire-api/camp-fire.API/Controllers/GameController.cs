using camp_fire.API.Configurations;
using camp_fire.Application.IServices;
using camp_fire.Application.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace camp_fire.API.Controllers;

[Route("[controller]")]
public class GameController : BaseApiController
{
    private readonly ILogger<GameController> _logger;
    private readonly IGameService _gameService;

    public GameController(ILogger<GameController> logger,
                            IGameService gameService
                            ) : base(logger)
    {
        _logger = logger;
        _gameService = gameService;
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _gameService.GetByIdAsync(id);
        return Ok(new BaseApiResult { Data = result });
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Post([FromBody] CreateGameRequestVM request)
    {
        var result = await _gameService.CreateAsync(request);
        return Ok(result);
    }

    [HttpPut]
    [AllowAnonymous]
    public async Task<IActionResult> Put([FromBody] UpdateGameRequestVM request)
    {
        var result = await _gameService.UpdateAsync(request);

        return Ok(result);
    }

    [HttpDelete("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> Delete(int id)
    {
        await _gameService.DeleteAsync(id);

        return Ok();
    }
}
