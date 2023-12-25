using AutoMapper;
using camp_fire.Application.IServices;
using camp_fire.Application.Models;
using camp_fire.Domain.Entities;
using camp_fire.Domain.SeedWork;
using camp_fire.Domain.SeedWork.Exceptions;

namespace camp_fire.Application.Services;

public class BoxService : IBoxService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public BoxService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<List<BoxResponseVM>> GetAsync(GetBoxRequestVM request)
    {
        var boxes = _unitOfWork.GetRepository<Box>().Find(x =>
            request.Id == null || x.Id == request.Id
            && (request.Id == null || x.Id == request.Id)
        ).ToList();

        var result = _mapper.Map<IList<Box>, List<BoxResponseVM>>(boxes);

        return await Task.FromResult(result);
    }

    public async Task<BoxResponseVM?> GetByIdAsync(int id)
    {
        var box = await _unitOfWork.GetRepository<Box>().GetByIdAsync(id);

        if (box is null)
            throw new ApiException("Box couldn't find ");

        var mappedBox = _mapper.Map<Box, BoxResponseVM>(box);

        return mappedBox;
    }

    public async Task<int> CreateAsync(CreateBoxRequestVM request)
    {
        var box = _mapper.Map<CreateBoxRequestVM, Box>(request);

        await _unitOfWork.GetRepository<Box>().CreateAsync(box);

        return await _unitOfWork.SaveChangesAsync();
    }

    public async Task<BoxResponseVM> UpdateAsync(UpdateBoxRequestVM request)
    {
        var box = await _unitOfWork.GetRepository<Box>().GetByIdAsync(request.Id);

        if (box is null)
            throw new ApiException("Box couldn't find");

        box = _mapper.Map<UpdateBoxRequestVM, Box>(request);

        _unitOfWork.GetRepository<Box>().Update(box);

        await _unitOfWork.SaveChangesAsync();

        var updatedExperience = await GetByIdAsync(request.Id);

        return updatedExperience!;
    }

    public async Task DeleteAsync(int id)
    {
        var box = await _unitOfWork.GetRepository<Box>().GetByIdAsync(id);

        if (box is null)
            throw new ApiException("Box couldn't find");

        _unitOfWork.GetRepository<Box>().Delete(box);
    }
}

