using camp_fire.Application.Models;
using camp_fire.Domain.Entities;

namespace camp_fire.Application.IServices;

public interface ICountryService
{
    Task<int> CreateAsync(Country request);
    Task<Country> UpdateAsync(Country request);
    Task<CountryResponseVM?> GetAsync(int id);
}
