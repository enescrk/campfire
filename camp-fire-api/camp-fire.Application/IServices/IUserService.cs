using camp_fire.Application.Models;
using camp_fire.Domain.Entities;

namespace camp_fire.Application.IServices;

public interface IUserService
{
    Task<int> CreateAsync(User request);
    Task<User> UpdateAsync(User request);
    Task<UserResponseVM?> GetAsync(int id);
}
