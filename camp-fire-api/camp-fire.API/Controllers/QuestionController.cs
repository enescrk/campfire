using camp_fire.API.Configurations;
using camp_fire.Application.IServices;
using camp_fire.Application.Models;
using camp_fire.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace camp_fire.API.Controllers;

[Route("[controller]")]
public class QuestionController : BaseApiController
{
    private readonly ILogger<QuestionController> _logger;
    private readonly IQuestionService _questionService;

    public QuestionController(ILogger<QuestionController> logger,
                            IQuestionService questionService
                            ) : base(logger)
    {
        _logger = logger;
        _questionService = questionService;
    }

     [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _questionService.GetByIdAsync(id);
        return Ok(new BaseApiResult { Data = result });
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Post([FromBody] CreateQuestionRequestVM request)
    {
        var result = await _questionService.CreateAsync(request);
        return Ok(result);
    }

    [HttpPut]
    [AllowAnonymous]
    public async Task<IActionResult> Put([FromBody] Question request)
    {
        // var result = await _eventService.UpdateAsync(request);

        return Ok();
    }
}
