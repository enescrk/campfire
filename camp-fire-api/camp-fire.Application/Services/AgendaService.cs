using AutoMapper;
using camp_fire.Application.IServices;
using camp_fire.Application.Models;
using camp_fire.Domain.Entities;
using camp_fire.Domain.SeedWork;
using camp_fire.Domain.SeedWork.Exceptions;

namespace camp_fire.Application.Services;

public class AgendaService : IAgendaService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AgendaService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<List<AgendaResponseVM>> GetAsync(GetAgendaRequestVM request)
    {
        var agendas = _unitOfWork.GetRepository<Agenda>().Find(x =>
        (request.Id == null || x.Id == request.Id)
        && (request.Description == null || x.Description == request.Description)
        && (request.Duration == null || x.Duration == request.Duration)
        && (request.Title == null || x.Title == request.Title)
        ).ToList();

        var result = _mapper.Map<IList<Agenda>, List<AgendaResponseVM>>(agendas);

        return await Task.FromResult(result);
    }
    public async Task<AgendaResponseVM?> GetByIdAsync(int id)
    {
        var agenda = await _unitOfWork.GetRepository<Agenda>().GetByIdAsync(id);

        if (agenda is null)
            throw new ApiException("Agenda couldn't find ");

        var result = _mapper.Map<Agenda,AgendaResponseVM>(agenda);

        return result;
    }

    public async Task<int> CreateAsync(CreateAgendaRequestVM request)
    {
        var agenda = new Agenda
        {
            Title = request.Title,
            Description = request.Description,
            Duration = request.Duration,
        };

        await _unitOfWork.GetRepository<Agenda>().CreateAsync(agenda);

        return await _unitOfWork.SaveChangesAsync();
    }


    public async Task<AgendaResponseVM> UpdateAsync(UpdateAgendaRequestVM request)
    {
        var agenda = await _unitOfWork.GetRepository<Agenda>().GetByIdAsync(request.Id);

        if (agenda is null)
            throw new ApiException("Agenda couldn't find");

        var mappedUpdateRequest = _mapper.Map<UpdateAgendaRequestVM, Agenda>(request);
        
        _unitOfWork.GetRepository<Agenda>().Update(mappedUpdateRequest);

        await _unitOfWork.SaveChangesAsync();

        var newAgenda = await _unitOfWork.GetRepository<Agenda>().GetByIdAsync(request.Id);

        var result = _mapper.Map<Agenda, AgendaResponseVM>(newAgenda);

        return result;
    }

    public async Task DeleteAsync(int id)
    {
        var agenda = await _unitOfWork.GetRepository<Agenda>().GetByIdAsync(id);

        if (agenda is null)
            throw new ApiException("Agenda couldn't find");

        _unitOfWork.GetRepository<Agenda>().Delete(agenda);
    }
}

