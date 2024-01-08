using Asp.Versioning;
using camp_fire.API.Configurations;
using camp_fire.Application.IServices;
using camp_fire.Application.Models;
using camp_fire.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace camp_fire.API.Controllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class AgendaController : BaseApiController
{
    private readonly ILogger<AddressController> _logger;
    private readonly IAgendaService _agendaService;

    public AgendaController(ILogger<AddressController> logger,
                            IAgendaService agendaService
                            ) : base(logger)
    {
        _logger = logger;
        _agendaService = agendaService;
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _agendaService.GetByIdAsync(id);
        return Ok(new BaseApiResult { Data = result });
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Post([FromBody] CreateAgendaRequestVM request)
    {
        var result = await _agendaService.CreateAsync(request);
        return Ok(result);
    }

    [HttpPut]
    [AllowAnonymous]
    public async Task<IActionResult> Put([FromBody] UpdateAgendaRequestVM request)
    {
        var result = await _agendaService.UpdateAsync(request);
        return Ok(result);
    }
    
    [HttpDelete]
    [AllowAnonymous]
    public async Task<IActionResult> Delete(int id)
    {
        await _agendaService.DeleteAsync(id);
        return Ok();
    }
}
