using camp_fire.Application.Models;
using camp_fire.Application.Models.Response;

namespace camp_fire.Application.IServices;

public interface IBookingService
{
    Task<int> CreateAsync(CreateBookingRequest request);
    Task<AddressResponseVM> UpdateAsync(UpdateAddressRequestVM request);
    Task<BookingResponse> GetByIdAsync(int id);
}
