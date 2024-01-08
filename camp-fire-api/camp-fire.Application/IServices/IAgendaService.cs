using camp_fire.Application.Models;

namespace camp_fire.Application.IServices;

public interface IAgendaService
{
    Task<int> CreateAsync(CreateAgendaRequestVM request);
    Task<AgendaResponseVM> UpdateAsync(UpdateAgendaRequestVM request);
    Task<AgendaResponseVM?> GetByIdAsync(int id);
    Task DeleteAsync(int id);
}
