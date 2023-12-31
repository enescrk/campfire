using camp_fire.API.Configurations;
using camp_fire.Application.IServices;
using camp_fire.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace camp_fire.API.Controllers;

[Route("[controller]")]
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

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Get([FromQuery] GetBookingsRequest request)
    {
        var result = await _bookingService.GetAsync(request);
        return Ok(new BaseApiResult { Data = result });
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _bookingService.GetByIdAsync(id);
        return Ok(new BaseApiResult { Data = result });
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Post([FromBody] CreateBookingRequest request)
    {
        var result = await _bookingService.CreateAsync(request);
        return Ok(result);
    }

    // [HttpPut]
    // [AllowAnonymous]
    // public async Task<IActionResult> Put([FromBody] UpdateBoxRequestVM request)
    // {
    //     var result = await _boxService.UpdateAsync(request);
    //     //TODO: Tekrar get isteği atılacak mı yoksa güncel entity dönecek mi?

    //     return Ok(new BaseApiResult { Data = result });
    // }
}
