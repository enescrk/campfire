using camp_fire.Application.Models;
using camp_fire.Application.Models.Request;
using camp_fire.Application.Models.Response;
using camp_fire.Domain.Entities;

namespace camp_fire.Application.IServices;

public interface IUserConfirmationService
{
    Task<int> CreateAsync(UserConfirmation request);
    Task<UserConfirmResponseVM> ConfirmAsync(UserConfirmRequestVM request);
    Task<UserConfirmation> UpdateAsync(UserConfirmation request);
    Task<UserConfirmationResponseVM?> GetAsync(int id);
}
