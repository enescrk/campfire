using camp_fire.Application.Models;

namespace camp_fire.Application.IServices;

public interface IUserService
{
    Task<int> CreateAsync(CreateUserRequestVM request);
    Task<UserResponseVM> UpdateAsync(UpdateUserRequestVM request);
    Task<UserResponseVM?> GetByIdAsync(int id);
    Task<List<UserResponseVM>> GetAsync(GetUserRequestVM request);
}
