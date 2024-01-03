using AutoMapper;
using camp_fire.Application.IServices;
using camp_fire.Application.Models;
using camp_fire.Application.Models.Request;
using camp_fire.Application.Models.Response;
using camp_fire.Application.Services;
using camp_fire.Domain.Entities;
using camp_fire.Domain.SeedWork;
using camp_fire.Domain.SeedWork.Exceptions;
using camp_fire.Infrastructure.Email;

public class CompanyService : ICompanyService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailService _emailService;
    private readonly IMapper _mapper;

    public CompanyService(IUnitOfWork unitOfWork,
                        IEmailService emailService,
                        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _emailService = emailService;
        _mapper = mapper;
    }

    public async Task CreateAsync(CreateCompanyRequest request)
    {
        if (request.ShortName.Length > 100)
            throw new ApiException("Name is too long (max 100 character)!");

        Address newAddress = null;

        if (request.Address != null)
        {
            newAddress = new Address
            {
                Title = request.Address.Title,
                CountryId = request.Address.CountryId,
                UserId = request.Address.UserId,
                City = request.Address.City,
                County = request.Address.County,
                OpenAddress = request.Address.OpenAddress
            };

            await _unitOfWork.GetRepository<Address>().CreateAsync(newAddress);
            await _unitOfWork.SaveChangesAsync();
        }

        var company = new Company
        {
            ShortName = request.ShortName,
            AddressId = newAddress?.Id,
        };

        await _unitOfWork.GetRepository<Company>().CreateAsync(company);

        await _unitOfWork.SaveChangesAsync();
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
        var experiences = _unitOfWork.GetRepository<Experience>().Find(x => !x.IsDeleted && experienceIds.Contains(x.Id)).ToList();

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

    public async Task<BookingResponse> UpdateAsync(UpdateBookingRequest request)
    {
        var booking = await _unitOfWork.GetRepository<Booking>().GetByIdAsync(request.Id);

        if (booking is null)
            throw new ApiException("Box couldn't find");


        booking.CompanyId = request.CompanyId;
        booking.Date = request.Date;
        booking.MeetingUrl = request.MeetingUrl;
        booking.ModeratorId = request.ModeratorId;

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