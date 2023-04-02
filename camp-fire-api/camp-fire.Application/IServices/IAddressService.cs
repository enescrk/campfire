using camp_fire.Application.Models;

namespace camp_fire.Application.IServices;

public interface IAddressService
{
    Task<int> CreateAsync(CreateAddressRequestVM request);
    Task<AddressResponseVM> UpdateAsync(UpdateAddressRequestVM request);
    Task<AddressResponseVM?> GetByIdAsync(int id);
}
