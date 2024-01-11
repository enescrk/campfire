using camp_fire.Application.Models;

namespace camp_fire.Application.IServices;

public interface IAgendaService
{
    Task<List<AgendaResponseVM>> GetAsync(GetAgendaRequestVM request);
    Task<int> CreateAsync(CreateAgendaRequestVM request);
    Task BulkCreateAsync(List<CreateAgendaRequestVM> requestList);
    Task<AgendaResponseVM> UpdateAsync(UpdateAgendaRequestVM request);
    Task<AgendaResponseVM?> GetByIdAsync(int id);
    Task DeleteAsync(int id);
}
