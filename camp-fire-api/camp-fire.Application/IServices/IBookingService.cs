using camp_fire.Application.Models;
using camp_fire.Application.Models.Response;

namespace camp_fire.Application.IServices;

public interface IBookingService
{
    Task<int> CreateAsync(CreateBookingRequest request);
    Task<BookingResponse> UpdateAsync(UpdateBookingRequest request);
    Task<BookingResponse> GetByIdAsync(int id);
    Task<List<BookingResponse>> GetAsync(GetBookingsRequest request);
    Task DeleteAsync(int id);
}
