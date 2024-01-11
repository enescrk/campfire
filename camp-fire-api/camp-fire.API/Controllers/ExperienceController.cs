using Asp.Versioning;
using camp_fire.API.Configurations;
using camp_fire.Application.IServices;
using camp_fire.Application.Models;
using camp_fire.Application.Token;
using camp_fire.Domain.SeedWork.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace camp_fire.API.Controllers;
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize]
public class ExperienceController : BaseApiController
{
    private readonly ILogger<ExperienceController> _logger;
    private readonly IExperienceService _experienceService;

    public ExperienceController(ILogger<ExperienceController> logger,
                            IExperienceService experienceService
                            ) : base(logger)
    {
        _logger = logger;
        _experienceService = experienceService;
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _experienceService.GetByIdAsync(id);
        return Ok(new BaseApiResult { Data = result });
    }

    [HttpGet("All")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll([FromQuery] GetExperienceRequest request)
    {
        var result = await _experienceService.GetllAsync(request);
        return Ok(new BaseApiResult { Data = result });
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Post([FromBody] CreateExperienceRequest request)
    {
        await _experienceService.CreateAsync(request);
        return Ok();
    }

    [HttpPut]
    [AllowAnonymous]
    public async Task<IActionResult> Put([FromBody] UpdateExperienceRequest request)
    {
        var result = await _experienceService.UpdateAsync(request);

        return Ok(result);
    }

    [HttpPut("admin/addModerator")]
    public async Task<IActionResult> AddModerator([FromBody] AddModeratorRequest request)
    {
        var loggedInUser = TokenProvider.GetLoggedInUser(User);

        if (!loggedInUser.IsManager)
            throw new ApiException("You don't have permission!");

        var result = await _experienceService.AddModerator(request);

        return Ok(result);
    }

    [HttpPut("admin/addBox")]
    public async Task<IActionResult> AddBox([FromBody] AddBoxRequest request)
    {
        var loggedInUser = TokenProvider.GetLoggedInUser(User);

        if (!loggedInUser.IsManager)
            throw new ApiException("You don't have permission!");

        var result = await _experienceService.AddBox(request);

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var loggedInUser = TokenProvider.GetLoggedInUser(User);

        if (!loggedInUser.IsManager)
            throw new ApiException("You don't have permission!");

        await _experienceService.DeleteAsync(id);

        return Ok();
    }
}
