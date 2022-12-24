using camp_fire.Application.Models;
using camp_fire.Domain.Entities;

namespace camp_fire.Application.IServices;

public interface IUserConfirmationService
{
    Task<int> CreateAsync(UserConfirmation request);
    Task<UserConfirmation> UpdateAsync(UserConfirmation request);
    Task<UserConfirmationResponseVM?> GetAsync(int id);
}
