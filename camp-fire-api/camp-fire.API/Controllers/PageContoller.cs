using camp_fire.API.Configurations;
using camp_fire.Application.IServices;
using camp_fire.Application.Models;
using camp_fire.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace camp_fire.API.Controllers;

[Route("[controller]")]
public class PageController : BaseApiController
{
    private readonly ILogger<PageController> _logger;
    private readonly IPageService _pageService;

    public PageController(ILogger<PageController> logger,
                            IPageService pageService
                            ) : base(logger)
    {
        _logger = logger;
        _pageService = pageService;
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _pageService.GetByIdAsync(id);
        return Ok(new BaseApiResult { Data = result });
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Post([FromBody] Page request)
    {
        var result = await _pageService.CreateAsync(request);
        return Ok(result);
    }

    [HttpPut]
    [AllowAnonymous]
    public async Task<IActionResult> Put([FromBody] PageResponseVM request)
    {
        var result = await _pageService.UpdateAsync(request);

        return Ok(result);
    }
}
