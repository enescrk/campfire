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
public class BookingController : BaseApiController
{
    private readonly ILogger<BookingController> _logger;
    private readonly IBookingService _bookingService;

    public BookingController(ILogger<BookingController> logger,
        IBookingService bookingService
                            ) : base(logger)
    {
        _logger = logger;
        _bookingService = bookingService;
    }

    [HttpGet("myBookings")]
    public async Task<IActionResult> Get([FromQuery] GetBookingsRequest request)
    {
        var loggedInUser = TokenProvider.GetLoggedInUser(User);

        request.CreatedBy = loggedInUser.Id;

        var result = await _bookingService.GetMyAsync(request);
        return Ok(new BaseApiResult { Data = result });
    }

    [HttpGet("admin")]
    public async Task<IActionResult> GetAll([FromQuery] GetBookingsRequest request)
    {
        var loggedInUser = TokenProvider.GetLoggedInUser(User);

        if (!loggedInUser.IsManager)
            throw new ApiException("You don't have permission!");

        var result = await _bookingService.GetAsync(request);
        return Ok(new BaseApiResult { Data = result });
    }

    // [HttpGet("{id}")]
    // [AllowAnonymous]
    // public async Task<IActionResult> Get(int id)
    // {
    //     var result = await _bookingService.GetByIdAsync(id);
    //     return Ok(new BaseApiResult { Data = result });
    // }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateBookingRequest request)
    {
        var loggedInUser = TokenProvider.GetLoggedInUser(User);
        request.CreatedBy = loggedInUser.Id;

        var result = await _bookingService.CreateAsync(request);
        return Ok(new BaseApiResult { Data = result });
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UpdateBookingRequest request)
    {
        var loggedInUser = TokenProvider.GetLoggedInUser(User);

        request.CreatedBy = loggedInUser.Id;

        var result = await _bookingService.UpdateAsync(request);

        return Ok(new BaseApiResult { Data = result });
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        var loggedInUser = TokenProvider.GetLoggedInUser(User);

        if (!loggedInUser.IsManager)
            throw new ApiException("You don't have permission for delete!");

        await _bookingService.DeleteAsync(id);

        return Ok(new BaseApiResult());
    }
}
