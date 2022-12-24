using camp_fire.API.Configurations;
using camp_fire.Application.IServices;
using camp_fire.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace camp_fire.API.Controllers;

[Route("[controller]")]
public class CountryController : BaseApiController
{
    private readonly ILogger<CountryController> _logger;
    private readonly ICountryService _countryService;

    public CountryController(ILogger<CountryController> logger,
                            ICountryService countryService
                            ) : base(logger)
    {
        _logger = logger;
        _countryService = countryService;
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _countryService.GetAsync(id);
        return Ok(new BaseApiResult { Data = result });
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Post([FromBody] Country request)
    {
        var result = await _countryService.CreateAsync(request);
        return Ok(result);
    }

    [HttpPut]
    [AllowAnonymous]
    public async Task<IActionResult> Put([FromBody] Country request)
    {
        var result = await _countryService.UpdateAsync(request);

        return Ok(result);
    }

    [HttpGet("test")]
    [AllowAnonymous]
    public async Task<IActionResult> Get([FromBody] Country request)
    {
        // var result = await _eventService.UpdateAsync(request);

        return Ok();
    }
}
