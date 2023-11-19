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
    private readonly IBoxService _boxService;

    public ExperienceService(IUnitOfWork unitOfWork,
                            IMapper mapper, IBoxService boxService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _boxService = boxService;
    }

    public async Task CreateAsync(CreateExperienceRequest request)
    {
        var experience = _mapper.Map<CreateExperienceRequest, Experience>(request);

        if (request.Box != null)
        {
           experience.BoxId = await _boxService.CreateAsync(request.Box);
        }

        await _unitOfWork.GetRepository<Experience>().CreateAsync(experience);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<ExperienceResponse?> GetByIdAsync(int id)
    {
        var experience = await _unitOfWork.GetRepository<Experience>().GetByIdAsync(id);

        if (experience is null)
            throw new ApiException("experience couldn't find ");

        var experienceModel = _mapper.Map<Experience, ExperienceResponse>(experience);
        
        if (experience.BoxId != null)
        {
            experienceModel.Box = await _boxService.GetByIdAsync(experience.BoxId.Value);
        }

        return experienceModel;
    }

    public async Task<List<ExperienceResponse>>? GetllAsync(GetExperienceRequest request)
    {
        var experiences = _unitOfWork.GetRepository<Experience>().Find(x =>

        request.Id == null || x.Id == request.Id
        && (string.IsNullOrEmpty(request.Title) || request.Title.ToLower().Contains(x.Title))
        && (request.Categories == null || x.Categories.Any(y => request.Categories.Any(z => z == y))))
        .Select(x => new ExperienceResponse
        {
            Id = x.Id,
            Categories = x.Categories,
            Content = x.Content,
            Title = x.Title,
            Summary = x.Summary,
            Currency = x.Currency,
            BannerImage = x.BannerImage,
            Duration = x.Duration,
            EnterpriceLevelId = x.EnterpriceLevelId,
            Header = x.Header,
            Image = x.Image,
            Images = x.Images,
            HeaderContent = x.HeaderContent,
            OwnerId = x.OwnerId,
            Price = x.Price,
            VideoUrl = x.VideoUrl
        }).ToList();

        return await Task.FromResult(experiences);
    }

    public async Task<ExperienceResponse> UpdateAsync(UpdateExperienceRequest request)
    {
        var experience = await _unitOfWork.GetRepository<Experience>().GetByIdAsync(request.Id);

        if (experience is null)
            throw new ApiException("experience couldn't find");

        experience = _mapper.Map<UpdateExperienceRequest, Experience>(request);

        _unitOfWork.GetRepository<Experience>().Update(experience);

        await _unitOfWork.SaveChangesAsync();

        var updatedExperience = await GetByIdAsync(request.Id);

        return updatedExperience!;
    }

    public async Task DeleteAsync(int id)
    {
        var experience = await _unitOfWork.GetRepository<Experience>().GetByIdAsync(id);

        if (experience is null)
            throw new ApiException("Experience couldn't find");

        _unitOfWork.GetRepository<Experience>().Delete(experience);
        await _unitOfWork.SaveChangesAsync();
    }
}