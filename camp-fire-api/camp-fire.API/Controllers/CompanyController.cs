using camp_fire.API.Configurations;
using camp_fire.Application.IServices;
using camp_fire.Application.Models;
using camp_fire.Application.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace camp_fire.API.Controllers;

[Route("[controller]")]
public class CompanyController : BaseApiController
{
    private readonly ILogger<CompanyController> _logger;
    private readonly ICompanyService _companyService;

    public CompanyController(ILogger<CompanyController> logger,
        ICompanyService companyService
                            ) : base(logger)
    {
        _logger = logger;
        _companyService = companyService;
    }

    // [HttpGet("{id}")]
    // [AllowAnonymous]
    // public async Task<IActionResult> Get(int id)
    // {
    //     var result = await _companyService.GetByIdAsync(id);
    //     return Ok(new BaseApiResult { Data = result });
    // }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Post([FromBody] CreateCompanyRequest request)
    {
        await _companyService.CreateAsync(request);
        return Ok(new BaseApiResult());
    }

    // [HttpPut]
    // [AllowAnonymous]
    // public async Task<IActionResult> Put([FromBody] UpdateBoxRequestVM request)
    // {
    //     var result = await _companyService.UpdateAsync(request);
    //     //TODO: Tekrar get isteği atılacak mı yoksa güncel entity dönecek mi?

    //     return Ok(new BaseApiResult { Data = result });
    // }
}