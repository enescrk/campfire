using camp_fire.API.Configurations;
using camp_fire.Application.IServices;
using camp_fire.Application.Models;
using camp_fire.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace camp_fire.API.Controllers;

[Route("[controller]")]
public class BoxController : BaseApiController
{
    private readonly ILogger<BoxController> _logger;
    private readonly IBoxService _boxService;

    public BoxController(ILogger<BoxController> logger,
        IBoxService boxService
                            ) : base(logger)
    {
        _logger = logger;
        _boxService = boxService;
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _boxService.GetByIdAsync(id);
        return Ok(new BaseApiResult { Data = result });
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Post([FromBody] CreateBoxRequestVM request)
    {
        var result = await _boxService.CreateAsync(request);
        return Ok(result);
    }

    [HttpPut]
    [AllowAnonymous]
    public async Task<IActionResult> Put([FromBody] UpdateBoxRequestVM request)
    {
        var result = await _boxService.UpdateAsync(request);
        //TODO: Tekrar get isteği atılacak mı yoksa güncel entity dönecek mi?

        return Ok(new BaseApiResult { Data = result });
    }
}
