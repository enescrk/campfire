using camp_fire.Application.IServices;
using camp_fire.Application.Models;
using camp_fire.Application.Models.Request;
using camp_fire.Application.Models.Response;
using camp_fire.Application.Token;
using camp_fire.Domain.Entities;
using camp_fire.Domain.SeedWork;
using camp_fire.Domain.SeedWork.Exceptions;
using Microsoft.Extensions.Configuration;

namespace camp_fire.Application.Services;

public class UserConfirmationService : IUserConfirmationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;

    public UserConfirmationService(IUnitOfWork unitOfWork,
                                     IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
    }

    public async Task<UserConfirmResponseVM> ConfirmAsync(UserConfirmRequestVM request)
    {
        #region User check

        var user = _unitOfWork.GetRepository<User>().FindOne(x => x.EMail == request.Email.ToLower().Trim());

        if (user is null)
            throw new ApiException("User could not be found");

        #endregion

        #region UserConfirm check

        var userConfirm = _unitOfWork.GetRepository<UserConfirmation>().FindOne(x => x.Key == request.Key && x.Secret == request.Code && x.UserId == user.Id);

        if (userConfirm is null)
            throw new ApiException("Key is not true?");

        #endregion

        #region Create token

        var loginedUser = new JwtTokenModel
        {
            Id = user.Id,
            Email = request.Email,
            FullName = $"{user?.Name} {user?.Surname}".Trim(),
            IsManager = user!.UserType == Domain.Enums.UserType.Admin
        };

        var tokenResult = TokenProvider.GenerateJwtToken(_configuration, loginedUser);

        #endregion

        return await Task.FromResult(new UserConfirmResponseVM
        {
            AccessToken = tokenResult!.AccessToken,
            Email = tokenResult.Email,
            ExpiresIn = tokenResult.ExpiresIn,
            FullName = tokenResult.FullName,
            Id = tokenResult.Id,
            RefreshToken = tokenResult.RefreshToken
        });
    }

    public Task<int> CreateAsync(UserConfirmation request)
    {
        throw new NotImplementedException();
    }

    public Task<UserConfirmationResponseVM?> GetAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<UserConfirmation> UpdateAsync(UserConfirmation request)
    {
        throw new NotImplementedException();
    }
}