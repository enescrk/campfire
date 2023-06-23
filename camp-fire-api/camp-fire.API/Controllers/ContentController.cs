using camp_fire.API.Configurations;
using camp_fire.Application.IServices;
using camp_fire.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace camp_fire.API.Controllers;

[Route("[controller]")]
public class ContentController : BaseApiController
{
    private readonly ILogger<ContentController> _logger;
    private readonly IContentService _contentService;

    public ContentController(ILogger<ContentController> logger,
                            IContentService contentService
                            ) : base(logger)
    {
        _logger = logger;
        _contentService = contentService;
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _contentService.GetByIdAsync(id);
        return Ok(new BaseApiResult { Data = result });
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Post([FromBody] Content request)
    {
        var result = await _contentService.CreateAsync(request);
        return Ok(result);
    }

    [HttpPut]
    [AllowAnonymous]
    public async Task<IActionResult> Put([FromBody] Content request)
    {
        var result = await _contentService.UpdateAsync(request);

        return Ok(result);
    }
}