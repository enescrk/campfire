using Asp.Versioning;
using camp_fire.API.Configurations;
using camp_fire.Application.IServices;
using camp_fire.Application.Models;
using camp_fire.Application.Models.Request;
using camp_fire.Application.Token;
using camp_fire.Domain.SeedWork.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace camp_fire.API.Controllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class UserController : BaseApiController
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserService _userService;

    public UserController(ILogger<UserController> logger,
                        IUserService userService) : base(logger)
    {
        _logger = logger;
        _userService = userService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var loggedInUser = TokenProvider.GetLoggedInUser(User);

        if (!loggedInUser.IsManager)
            throw new ApiException("You don't have permission!");

        var result = await _userService.GetByIdAsync(id);
        return Ok(new BaseApiResult { Data = result });
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(GetUsersRequest request)
    {
        var loggedInUser = TokenProvider.GetLoggedInUser(User);

        if (!loggedInUser.IsManager)
            throw new ApiException("You don't have permission!");

        var result = await _userService.GetAllAsync(request);
        
        return Ok(new BaseApiResult { Data = result });
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateUserRequestVM request)
    {
        var loggedInUser = TokenProvider.GetLoggedInUser(User);

        if (!loggedInUser.IsManager)
            throw new ApiException("You don't have permission!");

        var result = await _userService.CreateAsync(request);
        return Ok(result);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Post([FromBody] LoginRequestVM request)
    {
        var result = await _userService.LoginAsync(request);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UpdateUserRequestVM request)
    {
        var loggedInUser = TokenProvider.GetLoggedInUser(User);

        if (!loggedInUser.IsManager || loggedInUser.Id != request.Id)
            throw new ApiException("You don't have permission!");

        await _userService.UpdateAsync(request);

        return Ok();
    }
}
