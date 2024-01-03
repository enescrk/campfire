using camp_fire.Application.Models.Request;

namespace camp_fire.Application.IServices;

public interface ICompanyService
{
    Task CreateAsync(CreateCompanyRequest request);
}
