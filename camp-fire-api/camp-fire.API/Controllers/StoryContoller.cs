using camp_fire.API.Configurations;
using camp_fire.Application.IServices;
using camp_fire.Application.Models;
using camp_fire.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace camp_fire.API.Controllers;

[Route("[controller]")]
public class StoryController : BaseApiController
{
    private readonly ILogger<StoryController> _logger;
    private readonly IStoryService _storyService;

    public StoryController(ILogger<StoryController> logger,
                            IStoryService storyService
                            ) : base(logger)
    {
        _logger = logger;
        _storyService = storyService;
    }

     [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _storyService.GetByIdAsync(id);
        return Ok(new BaseApiResult { Data = result });
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Post([FromBody] CreateStoryRequestVM request)
    {
        var result = await _storyService.CreateAsync(request);
        return Ok(result);
    }

    [HttpPut]
    [AllowAnonymous]
    public async Task<IActionResult> Put([FromBody] Story request)
    {
        // var result = await _eventService.UpdateAsync(request);

        return Ok();
    }
}
