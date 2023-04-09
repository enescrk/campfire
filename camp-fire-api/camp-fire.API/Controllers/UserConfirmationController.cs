using camp_fire.API.Configurations;
using camp_fire.API.Hubs;
using camp_fire.Application.IServices;
using camp_fire.Application.Models.Request;
using camp_fire.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace camp_fire.API.Controllers;

[Route("[controller]")]
public class UserConfirmationController : BaseApiController
{
    private readonly ILogger<UserConfirmationController> _logger;
    private readonly IUserConfirmationService _usercomfirmationService;
    private readonly IHubContext<EventHub> _eventHub;

    public UserConfirmationController(ILogger<UserConfirmationController> logger,
                            IUserConfirmationService usercomfirmationService,
                            IHubContext<EventHub> eventHub
                            ) : base(logger)
    {
        _logger = logger;
        _usercomfirmationService = usercomfirmationService;
        _eventHub = eventHub;
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _usercomfirmationService.GetAsync(id);
        return Ok(new BaseApiResult { Data = result });
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Post([FromBody] UserConfirmation request)
    {
        var result = await _usercomfirmationService.CreateAsync(request);
        return Ok(result);
    }

    [HttpPut]
    [AllowAnonymous]
    public async Task<IActionResult> Put([FromBody] UserConfirmation request)
    {
        var result = await _usercomfirmationService.UpdateAsync(request);

        return Ok(result);
    }


    [HttpPost("confirm")]
    [AllowAnonymous]
    public async Task<IActionResult> Confirm([FromBody] UserConfirmRequestVM request)
    {
        var result = await _usercomfirmationService.ConfirmAsync(request);
        return Ok(result);
    }
}
