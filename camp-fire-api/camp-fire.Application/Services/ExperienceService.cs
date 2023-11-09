using AutoMapper;
using camp_fire.Application.IServices;
using camp_fire.Application.Models;
using camp_fire.Domain.Entities;
using camp_fire.Domain.SeedWork;
using camp_fire.Domain.SeedWork.Exceptions;

namespace camp_fire.Application.Services;

public class ExperienceService : IExperienceService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ExperienceService(IUnitOfWork unitOfWork,
                            IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<int> CreateAsync(CreateExperienceRequest request)
    {
        var experience = _mapper.Map<CreateExperienceRequest, Experience>(request);

        await _unitOfWork.GetRepository<Experience>().CreateAsync(experience);

        return await _unitOfWork.SaveChangesAsync();
    }

    public async Task<List<ExperienceResponse>>? GetllAsync(GetExperienceRequest request)
    {
        var experiences = _unitOfWork.GetRepository<Experience>().Find(x =>

        request.Id == null || x.Id == request.Id
        && (string.IsNullOrEmpty(request.Title) || request.Title.ToLower().Contains(x.Title)
        && request.Categories == null || x.Categories.Any(y => request.Categories.Any(z => z == y))
        ))
        .Select(x => new ExperienceResponse
        {
            Id = x.Id,
            Categories = x.Categories,
            Content = x.Content,
            Title = x.Title,
            Summary = x.Summary
        }).ToList();

        return await Task.FromResult(experiences);
    }

    public async Task<ExperienceResponse> UpdateAsync(UpdateExperienceRequest request)
    {
        throw new NotImplementedException();
    }
}