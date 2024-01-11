using AutoMapper;
using camp_fire.Application.IServices;
using camp_fire.Application.Models;
using camp_fire.Application.Models.Request;
using camp_fire.Application.Models.Response;
using camp_fire.Application.Token;
using camp_fire.Domain.Entities;
using camp_fire.Domain.SeedWork;
using camp_fire.Domain.SeedWork.Exceptions;
using camp_fire.Infrastructure.Email;

public class BookingService : IBookingService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailService _emailService;
    private readonly IMapper _mapper;
    private readonly IGoogleCalendarEventService _googleCalendarEventService;


    public BookingService(IUnitOfWork unitOfWork,
                        IEmailService emailService,
                        IMapper mapper,
                        IGoogleCalendarEventService googleCalendarEventService)
    {
        _unitOfWork = unitOfWork;
        _emailService = emailService;
        _mapper = mapper;
        _googleCalendarEventService = googleCalendarEventService;
    }

    public async Task<int> CreateAsync(CreateBookingRequest request)
    {
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
            CreatedBy = request.CreatedBy,
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

        #region Meeting oluşturma 

        var eventRequest = new CreateGoogleCalendarEventVM
        {
            StartDate = request.Date,
            Title = experience.Title,
            Description = experience.Summary,
            EndDate = request.Date.AddHours(2),
            Attendees = participantUsers.Select(x => x.EMail).ToList()
        };

        //Eventin biteceği dakika + 15 dk
        eventRequest.EndDate = eventRequest.StartDate.AddMinutes((double)(experience.Duration + 15)!);

        var meetResult = await _googleCalendarEventService.CreateEventAsync(eventRequest);

        booking.MeetingUrl = meetResult.HangoutLink;

        #endregion

        await _emailService.SendEmailAsync(new SendEmailModel
        {
            Subject = "Online Etkinlik Daveti",
            Body = $"Tebrikler! Online bir etkinliğe davet edildin. Etkinliğe zamanı geldiğinde aşağıdaki linkten ulaşabilirsin. \n Başlangıç tarihi: {request.Date} \n {booking.MeetingUrl}",
            To = participantUsers.Select(x => x.EMail).ToList()
        });

        return await _unitOfWork.SaveChangesAsync();
    }

    public async Task<List<BookingResponse>> GetMyAsync(GetBookingsRequest request)
    {
        var bookingList = _unitOfWork.GetRepository<Booking>().Find(x =>
                !x.IsDeleted && x.CreatedBy == request.CreatedBy
              && (request.Id == null || x.Id == request.Id)
              && (request.ExperienceId == null || x.ExperienceId == request.ExperienceId)
              && (request.CompanyId == null || x.CompanyId == request.CompanyId)
              && (request.OwnerId == null || x.OwnerId == request.OwnerId)
              && (request.ModeratorId == null || x.ModeratorId == request.ModeratorId)
              && (request.UserIds == null || x.UserIds.Any(y => request.UserIds.Contains(y)))
              && (request.StartDate == null || x.Date.Date >= request.StartDate.Value.Date)
              && (request.EndDate == null || x.Date.Date <= request.EndDate.Value.Date)).ToList();

        var experienceIds = bookingList.Select(x => x.ExperienceId).ToList();
        var experiences = _unitOfWork.GetRepository<Experience>().Find(x => !x.IsDeleted && experienceIds.Contains(x.Id)).ToList();

        var participantUsers = bookingList.SelectMany(x => x.UserIds).ToList();
        var users = _unitOfWork.GetRepository<User>().Find(x => !x.IsDeleted && participantUsers.Contains(x.Id)).ToList();

        var response = bookingList.Select(x => new BookingResponse
        {
            Id = x.Id,
            Date = x.Date,
            CreatedDate = x.CreatedDate,
            ParticipantUsers = users.Where(y => x.UserIds.Contains(y.Id)).Select(y => y.EMail).ToList(),
            UpdatedDate = x.UpdatedDate,
            ExperienceId = x.ExperienceId,
            ExperienceTitle = experiences.FirstOrDefault(y => y.Id == x.ExperienceId).Title
        }).ToList();

        return await Task.FromResult(response);
    }

    public async Task<List<BookingResponse>> GetAsync(GetBookingsRequest request)
    {
        var bookingList = _unitOfWork.GetRepository<Booking>().Find(x =>
                !x.IsDeleted
              && (request.Id == null || x.Id == request.Id)
              && (request.ExperienceId == null || x.ExperienceId == request.ExperienceId)
              && (request.CompanyId == null || x.CompanyId == request.CompanyId)
              && (request.OwnerId == null || x.OwnerId == request.OwnerId)
              && (request.ModeratorId == null || x.ModeratorId == request.ModeratorId)
              && (request.UserIds == null || x.UserIds.Any(y => request.UserIds.Contains(y)))
              && (request.StartDate == null || x.Date.Date >= request.StartDate.Value.Date)
              && (request.EndDate == null || x.Date.Date <= request.EndDate.Value.Date)).ToList();

        var experienceIds = bookingList.Select(x => x.ExperienceId).ToList();
        var experiences = _unitOfWork.GetRepository<Experience>().Find(x => !x.IsDeleted && experienceIds.Contains(x.Id)).ToList();

        var participantUsers = bookingList.SelectMany(x => x.UserIds).ToList();
        var users = _unitOfWork.GetRepository<User>().Find(x => !x.IsDeleted && participantUsers.Contains(x.Id)).ToList();

        var response = bookingList.Select(x => new BookingResponse
        {
            Id = x.Id,
            Date = x.Date,
            CreatedDate = x.CreatedDate,
            ParticipantUsers = users.Where(y => x.UserIds.Contains(y.Id)).Select(y => y.EMail).ToList(),
            UpdatedDate = x.UpdatedDate,
            ExperienceId = x.ExperienceId,
            ExperienceTitle = experiences.FirstOrDefault(y => y.Id == x.ExperienceId).Title
        }).ToList();

        return await Task.FromResult(response);
    }

    public async Task<BookingResponse> GetByIdAsync(int id)
    {
        var booking = await _unitOfWork.GetRepository<Booking>().GetByIdAsync(id);

        if (booking is null)
            throw new ApiException("Booking couldn't find ");

        var mappedBox = _mapper.Map<Booking, BookingResponse>(booking);

        return mappedBox;
    }

    public async Task<BookingResponse> UpdateAsync(UpdateBookingRequest request)
    {
        var booking = await _unitOfWork.GetRepository<Booking>().GetByIdAsync(request.Id);

        if (booking is null)
            throw new ApiException("Box couldn't find");


        booking.CompanyId = request.CompanyId;
        booking.Date = request.Date;
        booking.MeetingUrl = request.MeetingUrl;
        booking.ModeratorId = request.ModeratorId;
        booking.UpdatedBy = request.CreatedBy;

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

        var participantUsers = _unitOfWork.GetRepository<User>().Find(x => request.ParticipantMails.Contains(x.EMail)).ToList();

        booking.UserIds = participantUsers.Select(x => x.Id).ToList();

        _unitOfWork.GetRepository<Booking>().Update(booking);

        await _unitOfWork.SaveChangesAsync();

        var updatedExperience = await GetByIdAsync(request.Id);

        return updatedExperience!;
    }

    public async Task DeleteAsync(int id)
    {
        var booking = await _unitOfWork.GetRepository<Booking>().GetByIdAsync(id);

        if (booking is null)
            throw new ApiException("Booking couldn't find");

        _unitOfWork.GetRepository<Booking>().Delete(booking);
    }
}