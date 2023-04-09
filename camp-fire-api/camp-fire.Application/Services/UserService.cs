using camp_fire.Application.Configurations.Helpers;
using camp_fire.Application.IServices;
using camp_fire.Application.Models;
using camp_fire.Application.Models.Request;
using camp_fire.Domain.Entities;
using camp_fire.Domain.SeedWork;
using camp_fire.Domain.SeedWork.Exceptions;
using camp_fire.Infrastructure.Email;

namespace camp_fire.Application.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailService _emailService;

    public UserService(IUnitOfWork unitOfWork,
                       IEmailService emailService)
    {
        _unitOfWork = unitOfWork;
        _emailService = emailService;
    }

    public async Task<List<UserResponseVM>> GetAsync(GetUserRequestVM request)
    {
        var users = _unitOfWork.GetRepository<User>().Find(x =>
        request.Id == null || x.Id == request.Id
        && (request.Ids == null || request.Ids.Contains(x.Id))
        && (string.IsNullOrEmpty(request.Name) || x.Name!.ToLower() == request.Name.ToLower().Trim())
        && (string.IsNullOrEmpty(request.Surname) || x.Surname!.ToLower() == request.Surname.ToLower().Trim())
        && (string.IsNullOrEmpty(request.PhoneNumber) || x.PhoneNumber!.ToLower() == request.PhoneNumber.ToLower().Trim())
        && (string.IsNullOrEmpty(request.EMail) || x.EMail!.ToLower() == request.EMail.ToLower().Trim())
        )
        .Select(x => new UserResponseVM
        {
            Id = x.Id,
            Name = x!.Name!,
            Surname = x.Surname,
            AuthorizedCompanies = x.AuthorizedCompanies,
            Gender = x.Gender,
            EMail = x.EMail,
            UserType = x.UserType,
            PhoneNumber = x.PhoneNumber

        }).ToList();

        return await Task.FromResult(users);
    }

    public async Task<UserResponseVM?> GetByIdAsync(int id)
    {
        var user = await _unitOfWork.GetRepository<User>().GetByIdAsync(id);

        if (user is null)
            throw new ApiException("user couldn't find ");

        var mappedUser = MapUserResposeVMHelper(user);

        return mappedUser;
    }

    public async Task<int> CreateAsync(CreateUserRequestVM request)
    {
        var user = new User
        {
            Name = request.Name,
            Surname = request.Surname,
            EMail = request.EMail,
            Gender = request.Gender,
            UserType = request.UserType,
            PhoneNumber = request.PhoneNumber,
            IsActive = false //* ilk başta he default false setlenecek.
        };

        await _unitOfWork.GetRepository<User>().CreateAsync(user);

        return await _unitOfWork.SaveChangesAsync();
    }

    public async Task<string> LoginAsync(LoginRequestVM request)
    {
        #region User check

        var user = _unitOfWork.GetRepository<User>().FindOne(x => x.EMail!.ToLower().Contains(request.Email.ToLower().Trim()));

        if (user is null)
        {
            user = new User
            {
                EMail = request.Email.Trim(),
                Name = request.Name.Trim(),
                Surname = request.Surname.Trim()
            };

            await _unitOfWork.GetRepository<User>().CreateAsync(user);
        }

        #endregion

        #region UserConfirm

        var code = GenerateCodeHelper.GenerateCode(OnlyInt: true);

        var userConfirmation = new UserConfirmation
        {
            User = user,
            Key = code,
            Secret = GenerateCodeHelper.GenerateCode()
        };

        await _unitOfWork.GetRepository<UserConfirmation>().CreateAsync(userConfirmation);

        await _unitOfWork.SaveChangesAsync();

        #endregion

        #region SendEmail

        var sendEmailModel = new SendEmailModel
        {
            Subject = "Security Code",
            Body = code,
            To = new List<string> { request.Email! }
        };

        var result = await _emailService.SendEmailAsync(sendEmailModel);

        if (!result)
            throw new ApiException("Login Failed. Email could not send");

        #endregion

        return userConfirmation.Secret;
    }

    public async Task<UserResponseVM> UpdateAsync(UpdateUserRequestVM request)
    {
        var user = await _unitOfWork.GetRepository<User>().GetByIdAsync(request.Id);

        if (user is null)
            throw new ApiException("User couldn't find");

        user.Name = request.Name;
        user.Surname = request.Surname;
        user.AuthorizedCompanies = request.AuthorizedCompanies;
        user.EMail = request.EMail;
        user.Gender = request.Gender;
        user.UserType = request.UserType;
        user.IsActive = request.IsActive;

        _unitOfWork.GetRepository<User>().Update(user);

        await _unitOfWork.SaveChangesAsync();

        var newUser = await _unitOfWork.GetRepository<User>().GetByIdAsync(request.Id);

        var mappedUser = MapUserResposeVMHelper(newUser);

        return mappedUser;
    }

    public async Task DeleteAsync(int id)
    {
        var user = await _unitOfWork.GetRepository<User>().GetByIdAsync(id);

        if (user is null)
            throw new ApiException("User couldn't find");

        _unitOfWork.GetRepository<User>().Delete(user);
    }

    #region Helpers 

    private UserResponseVM MapUserResposeVMHelper(User user)
    {
        var userResponseVM = new UserResponseVM
        {
            Id = user.Id,
            Name = user.Name,
            Surname = user.Surname,
            EMail = user.EMail,
            Gender = user.Gender,
            UserType = user.UserType,
            PhoneNumber = user.PhoneNumber
        };
        return userResponseVM;
    }

    #endregion
}
