using AutoMapper;
using camp_fire.Application.Helpers;
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
    private readonly IAgendaService _agendaService;

    public ExperienceService(IUnitOfWork unitOfWork,
                            IAgendaService agendaService,
                            IMapper mapper,
                            IBoxService boxService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _agendaService = agendaService;
        _boxService = boxService;
    }

    public async Task CreateAsync(CreateExperienceRequest request)
    {
        var experience = _mapper.Map<CreateExperienceRequest, Experience>(request);

        var imageHelper = new ImageHelper();

        #region Add Box

        List<string> boxImages = null!;

        if (request.Box != null)
        {
            if (request.Box.Images != null && request.Box.Images.Any())
            {
                boxImages = new List<string>();

                foreach (var item in request.Box.Images)
                {
                    var image = imageHelper.SaveImage("experiences/images", item);
                    boxImages!.Add(image);
                }
            }

            var box = new Box
            {
                Description = request.Box.Description,
                Images = boxImages,
            };

            await _unitOfWork.GetRepository<Box>().CreateAsync(box);
            await _unitOfWork.SaveChangesAsync();

            experience.BoxId = box.Id;
        }

        #endregion

        #region Add Agendas

        if (request.Agendas != null)
        {
            var newAgendas = request.Agendas.Select(x => new Agenda
            {
                Title = x.Title,
                Description = x.Description,
                Duration = x.Duration,
            }).ToList();

            await _unitOfWork.GetRepository<Agenda>().BulkCreateAsync(newAgendas);

            experience.AgendaIds = newAgendas.Select(x => x.Id).ToList();
        }

        #endregion

        #region Add Images

        List<string> images = null!;

        if (request.Images != null && request.Images.Any())
        {
            images = new List<string>();

            foreach (var item in request.Images)
            {
                var image = imageHelper.SaveImage("experiences/images", item);
                images!.Add(image);
            }
        }

        string mainImage = null!;
        if (!string.IsNullOrEmpty(request.Image))
            mainImage = imageHelper.SaveImage("experiences/images", request.Image);

        string bannerImage = null!;
        if (!string.IsNullOrEmpty(request.BannerImage))
            bannerImage = imageHelper.SaveImage("experiences/images", request.BannerImage);

        experience.Images = images;
        experience.Image = mainImage;
        experience.BannerImage = bannerImage;

        #endregion

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
        .ToList();

        // var result = _mapper.Map<IList<Experience>, List<ExperienceResponse>>(experiences);

        List<AgendaResponseVM>? agendas = null;
        List<BoxResponseVM>? boxes = null;

        var agendaIds = experiences.Where(x => x.AgendaIds != null).SelectMany(x => x.AgendaIds)?.ToList();

        if (agendaIds.Count > 0)
            agendas = await _agendaService.GetAsync(new GetAgendaRequestVM { Ids = agendaIds });

        var boxIds = experiences.Where(x => x.BoxId.HasValue).Select(x => x.BoxId.Value).ToList();

        if (boxIds.Count > 0)
            boxes = await _boxService.GetAsync(new GetBoxRequestVM { Ids = boxIds });

        var response = experiences.Select(x => new ExperienceResponse
        {
            Id = x.Id,
            Agendas = agendas?.Where(y => x.AgendaIds.Contains(y.Id)).ToList(),
            AvailableDates = x.AvailableDates,
            BannerImage = x.BannerImage,
            Box = boxes?.FirstOrDefault(y => y.Id == x.BoxId),
            Categories = x.Categories,
            Content = x.Content,
            Currency = x.Currency,
            Duration = x.Duration,
            EnterpriceLevelId = x.EnterpriceLevelId,
            Header = x.Header,
            HeaderContent = x.HeaderContent,
            Image = x.Image,
            Images = x.Images,
            ModeratorId = x.ModeratorId,
            OwnerId = x.OwnerId,
            Price = x.Price,
            Summary = x.Summary,
            Title = x.Title,
            VideoUrl = x.VideoUrl,
            Warnings = x.Warnings,
            VideoContent = x.VideoContent
        }).ToList();

        return await Task.FromResult(response);
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

    public async Task<ExperienceResponse> AddModerator(AddModeratorRequest request)
    {
        var experience = await _unitOfWork.GetRepository<Experience>().GetByIdAsync(request.Id);

        if (experience is null)
            throw new ApiException("Experience couldn't find");

        var user = await _unitOfWork.GetRepository<User>().GetByIdAsync(request.ModeratorId);

        if (user is null)
            throw new ApiException("User couldn't find");

        experience.ModeratorId = request.ModeratorId;

        _unitOfWork.GetRepository<Experience>().Update(experience);

        await _unitOfWork.SaveChangesAsync();

        var updatedExperience = await GetByIdAsync(request.Id);

        return updatedExperience!;
    }

    public async Task<ExperienceResponse> AddBox(AddBoxRequest request)
    {
        var experience = await _unitOfWork.GetRepository<Experience>().GetByIdAsync(request.Id);

        if (experience is null)
            throw new ApiException("Experience couldn't find");

        var box = await _unitOfWork.GetRepository<Box>().GetByIdAsync(request.BoxId);

        if (box is null)
            throw new ApiException("Box couldn't find");

        experience.BoxId = request.BoxId;

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