using camp_fire.Application.IServices;
using camp_fire.Application.Models;
using camp_fire.Domain.Entities;
using camp_fire.Domain.SeedWork;
using camp_fire.Domain.SeedWork.Exceptions;

namespace camp_fire.Application.Services;

public class AddressService : IAddressService
{
    private readonly IUnitOfWork _unitOfWork;

    public AddressService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
  public async Task<List<AddressResponseVM>> GetAsync(GetAddressRequestVM request)
    {
        var addresses = _unitOfWork.GetRepository<Address>().Find(x =>
        request.Id == null || x.Id == request.Id
        && (request.Id == null || x.Id== request.Id)
    
        )
        .Select(x => new AddressResponseVM
        {
            CountryId=x.CountryId,
            UserId=x.UserId,
            City=x.City,
            County=x.County,
            OpenAddress=x.OpenAddress

        }).ToList();

        return await Task.FromResult(addresses);
    }
    public async Task<AddressResponseVM?> GetByIdAsync(int id)
    {
        var address = await _unitOfWork.GetRepository<Address>().GetByIdAsync(id);

        if (address is null)
            throw new ApiException("Address couldn't find ");

        var mappedAddress = MapAddressResposeVMHelper(address);

        return mappedAddress;
    }

    public async Task<int> CreateAsync(CreateAddressRequestVM request)
    {
        var address = new Address
        {
            CountryId=request.CountryId,
            UserId=request.UserId,
            City=request.City,
            County=request.County,
            OpenAddress=request.OpenAddress
        };

        await _unitOfWork.GetRepository<Address>().CreateAsync(address);

        return await _unitOfWork.SaveChangesAsync();
    }

    public async Task<AddressResponseVM> UpdateAsync(UpdateAddressRequestVM request)
    {
        var address = await _unitOfWork.GetRepository<Address>().GetByIdAsync(request.Id);

        if (address is null)
            throw new ApiException("Address couldn't find");

             address.CountryId=request.CountryId;
             address.UserId=request.UserId;
             address.City=request.City;
             address.County=request.County;
             address.OpenAddress=request.OpenAddress;
       
        _unitOfWork.GetRepository<Address>().Update(address);

        await _unitOfWork.SaveChangesAsync();

        var newAddress = await _unitOfWork.GetRepository<Address>().GetByIdAsync(request.Id);

        var mappedAddress = MapAddressResposeVMHelper(newAddress);

        return mappedAddress;
    }

    public async Task DeleteAsync(int id)
    {
        var address = await _unitOfWork.GetRepository<Address>().GetByIdAsync(id);

        if (address is null)
            throw new ApiException("Address couldn't find");

        _unitOfWork.GetRepository<Address>().Delete(address);
    }

    #region Helpers 

    private AddressResponseVM MapAddressResposeVMHelper(Address address)
    {
        var addressResponseVM = new AddressResponseVM
        {
            CountryId=address.CountryId,
            UserId=address.UserId,
            City=address.City,
            County=address.County,
            OpenAddress=address.OpenAddress
        };
        return addressResponseVM;
    }

    #endregion
}

