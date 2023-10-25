using camp_fire.Application.IServices;
using camp_fire.Application.Models;
using camp_fire.Application.Models.Request;
using camp_fire.Domain.Entities;
using camp_fire.Domain.SeedWork;
using camp_fire.Domain.SeedWork.Exceptions;
using camp_fire.Infrastructure.Email;

namespace camp_fire.Application.Services;

public class EventService : IEventService
{
    //TODO: Etkinliğer davetli ekleme-çıkarma servisleri hazırlanacak.
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailService _emailService;

    public EventService(IUnitOfWork unitOfWork,
                        IEmailService emailService)
    {
        _unitOfWork = unitOfWork;
        _emailService = emailService;
    }

    public async Task<int> CreateAsync(CreateEventReqeustVM request)
    {
        //TODO: Etklinliği oluşturan kullanıcının bilgileri çekilecek. bunun için login oluşturulacak. Token'ı üzerinden Id'si etkinliğe setlenecek. Mail adresi de davetlielerer gönderilen mail'e eklenecek.(Soru iptal erteleme işlemlerine oluşturan kişi bakacak.)
        //TODO: Email template'i oluşturulacak.
        //TODO: Emaideki linke tıklandığında servise istek atılacak ve etkinlik zamanı ve yetki kontrolleri bu serviste olacak.
        var games = _unitOfWork.GetRepository<Game>().Find(x => request.GameIds.Contains(x.Id)).ToList();

        if (games is null)
            throw new ApplicationException("Games couldn't found");

        #region Var olmayan kullanıcıları bulup db'ye ekle

        var requestedEmails = request.ParticipiantUsers.Select(x => x.EMail).ToList();

        var users = _unitOfWork.GetRepository<User>().Find(x => requestedEmails.Contains(x.EMail)).ToList();

        var existUserMails = users.Select(x => x.EMail).ToList();

        var newUserList = new List<User>();

        foreach (var item in request.ParticipiantUsers)
        {
            var userExist = existUserMails.FirstOrDefault(x => x == item.EMail);

            if (userExist is null)
            {
                var user = new User
                {
                    Name = item.Name,
                    Surname = item.Surname,
                    EMail = item.Surname,
                    PhoneNumber = item.PhoneNumber
                };

                newUserList.Add(user);
            }
        }

        await _unitOfWork.GetRepository<User>().BulkCreateAsync(newUserList);

        #endregion

        #region Eventi oluşturma

        var newEvent = new Event
        {
            Name = request.Name,
            CompanyId = request.CompanyId,
            Date = request.Date,
            MeetingUrl = $"https://campfire.com/event", //TODO: kaldırılabilir
            HashedKey = "", //TODO: kaldırılabilir,
            UserId = 3,
        };

        await _unitOfWork.GetRepository<Event>().CreateAsync(newEvent);

        #endregion

        #region Page kayıtlarını oluşturma

        var newPages = new List<Page>();

        foreach (var item in games)
        {
            var page = new Page
            {
                Event = newEvent,
                CreatedBy = 3,
                Game = item,
                IsCompleted = false,
                Name = item.Name
            };
            newPages.Add(page);
        }

        await _unitOfWork.GetRepository<Page>().BulkCreateAsync(newPages);

        #endregion

        #region Scoreboard ve Event-User kayıtlarını oluşturma
        //* Scoreboard kaydı her Oyuncunun Event için her oyun için ayrı bir kaydı oluşturulur. Her oyuncunun her oyundaki score kaydını bulabilmemizi sağlar.

        var participiantUsers = new List<User>();
        var newParticipiantList = new List<EventParticipant>();

        if (users is null)
            participiantUsers.AddRange(newUserList);
        else
            participiantUsers.AddRange(users);

        var newScorboards = new List<Scoreboard>();
        var newEvetParticipiants = new List<EventParticipant>();

        foreach (var user in participiantUsers)
        {
            var eventParticipant = new EventParticipant
            {
                Event = newEvent,
                User = user
            };

            newEvetParticipiants.Add(eventParticipant);
        }

        // await _unitOfWork.GetRepository<Scoreboard>().BulkCreateAsync(newScorboards);
        await _unitOfWork.GetRepository<EventParticipant>().BulkCreateAsync(newEvetParticipiants);

        #endregion

        //TODO:Emai'e login sayfasının url'i + eventId gidecek.
        //*Login
        await _emailService.SendEmailAsync(new SendEmailModel
        {
            Subject = "Online Kamp Daveti",
            Body = $"Arkadaşın seni eğlenceli oyunları içeren online bir etkinliğe davet etti!. Etkinliğe zamanı geldiğinde aşağıdaki linkten ulaşabilirsin. \n Başlangıç tarihi: {request.Date.Date} \n",
            To = requestedEmails
        });

        return await _unitOfWork.SaveChangesAsync();
    }

    public async Task<EventResponseVM> UpdateAsync(UpdateEventRequestVM request)
    {
        var eventt = await _unitOfWork.GetRepository<Event>().GetByIdAsync(request.Id);

        if (eventt is null)
            throw new ApiException("Event couldn't find");

        eventt.Name = request.Name;
        eventt.Date = request.Date;
        eventt.HashedKey = request.HashedKey;
        eventt.MeetingUrl = request.MeetingUrl;
        eventt.ParticipiantIds = request.ParticipiantIds;
        eventt.PageIds = request.GameIds.ToArray(); //TODO: düzenlenecek.
        eventt.CompanyId = request.CompanyId;
        eventt.EventData = request.EventData;

        _unitOfWork.GetRepository<Event>().Update(eventt);

        await _unitOfWork.SaveChangesAsync();

        var newEvent = await _unitOfWork.GetRepository<Event>().GetByIdAsync(request.Id);

        var mappedEvent = MapEventResposeVMHelper(newEvent);

        return mappedEvent;
    }

    public async Task<EventResponseVM> UpdateCurrentPageAsync(UpdatePageRequestVM request)
    {
        var eventt = await _unitOfWork.GetRepository<Event>().GetByIdAsync(request.Id);

        if (eventt is null)
            throw new NullReferenceException("Event couldn't find");

        var page = eventt.Pages.FirstOrDefault(x => x.Id == request.PageId);

        if (page is null)
            throw new ApiException("Page couldn't find");

        page.IsCompleted = true;

        eventt.CurrentPageId = eventt!.Pages!.FirstOrDefault(x => !x.IsCompleted)!.Id;

        if (eventt.CurrentPageId is null)
            throw new Exception("Event is ended");

        _unitOfWork.GetRepository<Page>().Update(page);
        _unitOfWork.GetRepository<Event>().Update(eventt);

        await _unitOfWork.SaveChangesAsync();

        var mappedEvent = MapEventResposeVMHelper(eventt);

        return mappedEvent;
    }

    public async Task<EventResponseVM> UpdateCurrentUserAsync(UpdateCurrentUserRequestVM request)
    {
        var eventt = await _unitOfWork.GetRepository<Event>().GetByIdAsync(request.Id);

        if (eventt is null)
            throw new NullReferenceException("Event couldn't find");

        if (eventt.Date.Date <= DateTime.Now || eventt.IsCompleted) //* şu anki tarih eventin tarihinden önceyse veya event bitmişse hata fırlat.
            throw new Exception("Event is not playable!");

        var user = _unitOfWork.GetRepository<User>().FindOne(x => x.Id == request.UserId);

        if (user is null)
            throw new NullReferenceException("Event couldn't find!");

        if (!eventt.EventParticipants.Any(x => x.UserId == user.Id))
            throw new NullReferenceException("User is not participiant of this Event!");

        // eventt.CurrentUserId =eventt.u//TODO:scoreboard listesindeki kayıtlara göre yapılabilir.
        //* Oynanacak el sayısı event oluştururken belirlenebilir olacak. Scoreboard tablosunda ve Pagetablosunda kayıt tutulacak.
        // scoreboard tablosunda oyuncnunu kaç el oynadığı tutulacak her el güncellenecek.

        _unitOfWork.GetRepository<Event>().Update(eventt);

        await _unitOfWork.SaveChangesAsync();

        var mappedEvent = MapEventResposeVMHelper(eventt);

        return mappedEvent;
    }

    public async Task<EventResponseVM> UpdateActiveUserAsync(ActivateUserRequestVM request)
    {
        var eventt = await _unitOfWork.GetRepository<Event>().GetByIdAsync(request.Id);

        if (eventt is null)
            throw new NullReferenceException("Event couldn't find");

        if (eventt.Date.Date <= DateTime.Now || eventt.IsCompleted) //* şu anki tarih eventin tarihinden önceyse veya event bitmişse hata fırlat.
            throw new Exception("Event is not playable!");

        var user = _unitOfWork.GetRepository<User>().FindOne(x => x.EMail == request.Email.ToLower().Trim());

        if (user is null)
            throw new NullReferenceException("Event couldn't find!");

        if (!eventt.EventParticipants.Any(x => x.UserId == user.Id))
            throw new NullReferenceException("User is not participiant of this Event!");

        var updateUser = eventt.EventParticipants.FirstOrDefault(x => x.UserId == user.Id)!.User;

        updateUser.IsActive = request.IsActive;

        _unitOfWork.GetRepository<User>().Update(updateUser);
        _unitOfWork.GetRepository<Event>().Update(eventt);

        await _unitOfWork.SaveChangesAsync();

        var mappedEvent = MapEventResposeVMHelper(eventt);

        return mappedEvent;
    }

    public async Task<EventResponseVM?> GetAsync(int id)
    {
        List<UserResponseVM>? users = null;

        var eventt = _unitOfWork.GetRepository<Event>().FindOne(x => x.Id == id);

        if (eventt.ParticipiantIds is not null)
        {
            users = new List<UserResponseVM>();
            users = _unitOfWork.GetRepository<User>().Find(x => eventt.ParticipiantIds.Contains(x.Id)).
            Select(x => new UserResponseVM
            {
                Id = x.Id,
                EMail = x.EMail,
                IsActive = x.IsActive,
                Name = x.Name,
                Surname = x.Surname,
                Gender = x.Gender,
                PhoneNumber = x.PhoneNumber,
                UserType = x.UserType
            }).ToList();
        }

        var result = new EventResponseVM
        {
            Id = eventt.Id,
            HashedKey = eventt.HashedKey,
            Name = eventt.Name,
            Date = eventt.Date,
            EventData = eventt.EventData,
            User = new UserResponseVM
            {
                Id = eventt.UserId,
                Name = eventt.User?.Name,
                Surname = eventt.User?.Surname
            },
            Users = users,
            ParticipiantIds = eventt.ParticipiantIds?.ToList(),
            PageIds = eventt.Pages?.Select(x => x.Id).ToList(),
            Pages = eventt.Pages?.Select(x => new PageResponseVM
            {
                Id = x.Id,
                IsCompleted = x.IsCompleted,
                Name = x.Name,
                ScoreboardId = x.Scoreboard?.Id
            }).ToList(),
            Scoreboards = eventt.Scoreboards?.Select(x => new ScoreboardResponseVM
            {
                Id = x.Id,
                EventId = x.EventId,
                PageId = x.PageId,
                UserId = x.UserId,
                Score = x.Score
            }).ToList()
        };

        return await Task.FromResult(result);
    }

    public async Task<List<EventResponseVM>?> GetAsync(GetEventsRequestVM request)
    {
        var eventList = _unitOfWork.GetRepository<Event>().Find(x => !x.IsDeleted
        && (request.Id == null || x.Id == request.Id)
        && (request.Date == null || x.Date == request.Date)
        && (request.UserId == null || x.UserId == request.UserId)
        && (request.CurrentUserId == null || x.CurrentUserId == request.CurrentUserId)
        && (request.CurrentPageId == null || x.CurrentPageId == request.CurrentPageId)
        && (request.CompanyId == null || x.CompanyId == request.CompanyId)
        && (request.ParticipiantId == null || (x.ParticipiantIds != null && x.ParticipiantIds.Contains(request.ParticipiantId!.Value)))
        && (string.IsNullOrEmpty(request.Name) || x.Name == request.Name!.ToLower().Trim())
        && (string.IsNullOrEmpty(request.HashedKey) || x.HashedKey == request.HashedKey!.ToLower().Trim())
        && (string.IsNullOrEmpty(request.MeetingUrl) || x.MeetingUrl == request.MeetingUrl!.ToLower().Trim()))
        .Select(x => new EventResponseVM
        {
            Id = x.Id,
            HashedKey = x.HashedKey,
            Name = x.Name,
            Date = x.Date,
            User = new UserResponseVM
            {
                Id = x.UserId,
                Name = x.User.Name,
                Surname = x.User.Surname
            },
            ParticipiantIds = x.ParticipiantIds.ToList(),
            PageIds = x.Pages.Select(y => x.Id).ToList(),
            Pages = x.Pages.Select(y => new PageResponseVM
            {
                Id = y.Id,
                IsCompleted = y.IsCompleted,
                Name = y.Name,
                ScoreboardId = y.Scoreboard.Id
            }).ToList(),
            Scoreboards = x.Scoreboards.Select(y => new ScoreboardResponseVM
            {
                Id = y.Id,
                EventId = y.EventId,
                PageId = y.PageId,
                UserId = y.UserId,
                Score = y.Score
            }).ToList()
        }).ToList();

        return await Task.FromResult(eventList);
    }

    public async Task DeleteAsync(int id)
    {
        var eventt = await _unitOfWork.GetRepository<Event>().GetByIdAsync(id);

        if (eventt is null)
            throw new ApiException("event couldn't find");

        _unitOfWork.GetRepository<Event>().Delete(eventt);
    }

    public async Task SaveChangesAsync()
    {
        await _unitOfWork.SaveChangesAsync();
    }

    #region Helpers 

    private EventResponseVM MapEventResposeVMHelper(Event eventt)
    {
        var eventResponseVM = new EventResponseVM
        {
            Id = eventt.Id,
            Name = eventt.Name,
            Date = eventt.Date,
            HashedKey = eventt.HashedKey,
            PageIds = eventt.Pages.Select(x => x.Id).ToList(),
            Pages = eventt.Pages?.Select(x => new PageResponseVM
            {
                Id = x.Id,
                IsCompleted = x.IsCompleted,
                Name = x.Name,
                ScoreboardId = x.Scoreboard?.Id
            }).ToList(),
            User = new UserResponseVM
            {
                Id = eventt.UserId,
                Name = eventt.User?.Name,
                Surname = eventt.User?.Surname
            },
            Scoreboards = eventt.Scoreboards?.Select(x => new ScoreboardResponseVM
            {
                Id = x.Id,
                EventId = x.EventId,
                PageId = x.PageId,
                UserId = x.UserId,
                Score = x.Score
            }).ToList()
        };

        return eventResponseVM;
    }

    #endregion
}