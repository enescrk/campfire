using camp_fire.Application.Models;
using camp_fire.Domain.Entities;

namespace camp_fire.Application.IServices;

public interface IAddressService
{
    Task<int> CreateAsync(Address request);
    Task<Address> UpdateAsync(Address request);
    Task<AddressResponseVM?> GetAsync(int id);
}
