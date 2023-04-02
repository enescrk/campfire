using camp_fire.API.Configurations;
using camp_fire.API.Hubs;
using camp_fire.API.Models;
using camp_fire.Application.IServices;
using camp_fire.Application.Models;
using camp_fire.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace camp_fire.API.Controllers;

[Route("[controller]")]
public class EventController : BaseApiController
{
    private readonly ILogger<EventController> _logger;
    private readonly IEventService _eventService;
    private readonly IHubContext<EventHub> _eventHub;
    private readonly IUserService _userService;
    private readonly IPageService _pageService;

    public EventController(ILogger<EventController> logger,
                            IEventService eventService,
                            IHubContext<EventHub> eventHub,
                            IUserService userService,
                            IPageService pageService
                            ) : base(logger)
    {
        _logger = logger;
        _eventService = eventService;
        _eventHub = eventHub;
        _userService = userService;
        _pageService = pageService;
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _eventService.GetAsync(id);
        return Ok(new BaseApiResult { Data = result });
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Get(GetEventsRequestVM request)
    {
        var result = await _eventService.GetAsync(request);
        return Ok(new BaseApiResult { Data = result });
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Post([FromBody] Event request)
    {
        var result = await _eventService.CreateAsync(request);
        return Ok(result);
    }

    [HttpPut]
    [AllowAnonymous]
    public async Task<IActionResult> Put([FromBody] UpdateEventRequestVM request)
    {
        var result = await _eventService.UpdateAsync(request);

        var eventHubModel = MapEventHubModelHelper(result);

        eventHubModel.ParticipiantUsers = await _userService.GetAsync(new GetUserRequestVM { Ids = eventHubModel.ParticipiantIds });

        await _eventHub.Clients.All.SendAsync("GetEvent", eventHubModel);

        return Ok(result);
    }

    [HttpGet("test")]
    [AllowAnonymous]
    public async Task<IActionResult> Get()
    {
        return Ok();
    }

    [HttpPut("changeTurn")]
    [AllowAnonymous]
    public async Task<IActionResult> ChangeTurn()
    {
        //TODO: Parametre olarak eventId alacak çünkü isteği atan tokendan user'ı bulacağız. tek bir aktif eventi olabilecekse parametresiz istek atması yeterli olacak
        return Ok();
    }

    ///summary
    //*CurrentPage'i güncelleme
    [HttpPut("updatePage")]
    [AllowAnonymous]
    public async Task<IActionResult> UpdatePage(UpdatePageRequestVM requestVM)
    {
        //TODO: Bu servisin aktif çalışabilmesi için önceki oyunun düzgün bir şekilde tamamlanıp tamamlanmadığı kontrol edilmeli. 
        //* sonrasında verilen page'den sonra bir page id varsa currentPageId üzerine setlenmeli.
        var resultEvent = await _eventService.UpdateCurrentPageAsync(requestVM);

        var eventHubModel = MapEventHubModelHelper(resultEvent!);

        await _eventHub.Clients.All.SendAsync("GetEvent", eventHubModel);

        return Ok(eventHubModel);
    }

    ///summary
    // oyuna o an katılan kullanıcıları güncelleme işlemi
    [HttpPut("checkActiveUser")]
    [AllowAnonymous]
    public async Task<IActionResult> CheckActiveUser(int eventId, string mail, bool isActive)
    {
        //event i çek
        //user listesindeki user'ı bul ve IsAvtive'i güncelle
        //modeli dön
        var eventt = await _eventService.GetAsync(eventId);

        if (eventt is null)
            throw new ApplicationException("There is no event with that Id!");

        var user = eventt.Users.FirstOrDefault(x => x.EMail == mail);

        if (user is not null)
            user.IsActive = isActive;

        var eventHubModel = MapEventHubModelHelper(eventt);

        await _eventHub.Clients.All.SendAsync("GetEvent", eventHubModel);

        return Ok(eventHubModel);
    }

    #region Helpers

    private EventHubResponseVM MapEventHubModelHelper(EventResponseVM eventt)
    {
        //TODO: Modele Page listesi eklenecek bir kaç yeri etiliyor bu yüzden ya extention mapleme olacak yada auto mapper eklenecek.
        var eventHubModel = new EventHubResponseVM
        {
            Id = eventt.Id,
            CurrentPageId = eventt.CurrentPageId,
            Date = eventt.Date,
            Name = eventt.Name,
            PageIds = eventt.PageIds,
            ParticipiantIds = eventt.ParticipiantIds,
            Scoreboards = eventt.Scoreboards?.Select(x => new ScoreboardResponseVM
            {
                Id = x.Id,
                EventId = x.EventId,
                PageId = x.PageId,
                UserId = x.UserId,
                Score = x.Score
            }).ToList()
        };

        return eventHubModel;
    }

    #endregion
}
