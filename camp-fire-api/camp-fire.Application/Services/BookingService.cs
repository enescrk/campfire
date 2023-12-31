using AutoMapper;
using camp_fire.Application.IServices;
using camp_fire.Application.Models;
using camp_fire.Application.Models.Response;
using camp_fire.Domain.Entities;
using camp_fire.Domain.SeedWork;
using camp_fire.Domain.SeedWork.Exceptions;
using camp_fire.Infrastructure.Email;

public class BookingService : IBookingService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailService _emailService;
    private readonly IMapper _mapper;


    public BookingService(IUnitOfWork unitOfWork,
                        IEmailService emailService,
                        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _emailService = emailService;
        _mapper = mapper;
    }

    public async Task<int> CreateAsync(CreateBookingRequest request)
    {
        //TODO: Etklinliği oluşturan kullanıcının bilgileri çekilecek. bunun için login oluşturulacak. Token'ı üzerinden Id'si etkinliğe setlenecek. Mail adresi de davetlielerer gönderilen mail'e eklenecek.(Soru iptal erteleme işlemlerine oluşturan kişi bakacak.)
        //TODO: Email template'i oluşturulacak.
        //TODO: Emaideki linke tıklandığında servise istek atılacak ve etkinlik zamanı ve yetki kontrolleri bu serviste olacak.

        #region Experience Check

        var experience = await _unitOfWork.GetRepository<Experience>().FindOneAsync(x => x.Id == request.ExperienceId);

        if (experience is null)
            throw new ApiException("Experience could not be found!");

        #endregion

        #region Var olmayan kullanıcıları bulup db'ye ekle

        var users = _unitOfWork.GetRepository<User>().Find(x => request.ParticipantMails.Contains(x.EMail)).ToList();

        var existUserMails = users.Select(x => x.EMail).ToList();

        var newUserList = new List<User>();

        foreach (var item in request.ParticipantMails)
        {
            var mail = existUserMails.FirstOrDefault(x => x == item);

            if (mail is null)
            {
                var user = new User
                {
                    EMail = item
                };

                newUserList.Add(user);
            }
        }

        await _unitOfWork.GetRepository<User>().BulkCreateAsync(newUserList);
        await _unitOfWork.SaveChangesAsync();

        #endregion

        #region Booking oluşturma

        var participantUsers = _unitOfWork.GetRepository<User>().Find(x => request.ParticipantMails.Contains(x.EMail)).ToList();

        var booking = new Booking
        {
            ExperienceId = request.ExperienceId,
            ModeratorId = request.ModeratorId,
            OwnerId = request.OwnerId,
            UserIds = participantUsers.Select(x => x.Id).ToList(),
            Date = request.Date.Date,
            CompanyId = request.CompanyId,
            RecordUrl = request.RecordUrl,
            MeetingUrl = request.MeetingUrl
        };

        await _unitOfWork.GetRepository<Booking>().CreateAsync(booking);

        #endregion

        //TODO:Emai'e login sayfasının url'i + eventId gidecek.
        //*Login
        await _emailService.SendEmailAsync(new SendEmailModel
        {
            Subject = "Online Etkinlik Daveti",
            Body = $"Tebrikler! Online bir etkinliğe davet edildin. Etkinliğe zamanı geldiğinde aşağıdaki linkten ulaşabilirsin. \n Başlangıç tarihi: {request.Date.Date} \n {request.MeetingUrl}",
            To = participantUsers.Select(x => x.EMail).ToList()
        });

        return await _unitOfWork.SaveChangesAsync();
    }

    public async Task<List<BookingResponse>> GetAsync(GetBookingsRequest request)
    {
        var bookingList = _unitOfWork.GetRepository<Booking>().Find(x => !x.IsDeleted
              && (request.Id == null || x.Id == request.Id)
              && (request.ExperienceId == null || x.ExperienceId == request.ExperienceId)
              && (request.CompanyId == null || x.CompanyId == request.CompanyId)
              && (request.OwnerId == null || x.OwnerId == request.OwnerId)
              && (request.ModeratorId == null || x.ModeratorId == request.ModeratorId)
              && (request.UserIds == null || x.UserIds.Any(y => request.UserIds.Contains(y)))
              && (request.StartDate == null || x.Date.Date >= request.StartDate.Value.Date)
              && (request.EndDate == null || x.Date.Date <= request.EndDate.Value.Date))
              .Select(x => new BookingResponse
              {
                  Id = x.Id,
                  Date = x.Date,
                  CreatedDate = x.CreatedDate,
                  ExperienceId = x.ExperienceId
              }).ToList();

        var experienceIds = bookingList.Select(x => x.ExperienceId).ToList();
        var experiences  = _unitOfWork.GetRepository<Experience>().Find(x => !x.IsDeleted && experienceIds.Contains(x.Id)).ToList();

        return await Task.FromResult(bookingList);
    }

    public async Task<BookingResponse> GetByIdAsync(int id)
    {
        var booking = await _unitOfWork.GetRepository<Booking>().GetByIdAsync(id);

        if (booking is null)
            throw new ApiException("Booking couldn't find ");

        var mappedBox = _mapper.Map<Booking, BookingResponse>(booking);

        return mappedBox;
    }

    public Task<AddressResponseVM> UpdateAsync(UpdateAddressRequestVM request)
    {
        throw new NotImplementedException();
    }
}