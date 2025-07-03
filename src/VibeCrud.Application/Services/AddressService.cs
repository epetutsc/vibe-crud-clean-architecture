using VibeCrud.Application.DTOs;
using VibeCrud.Application.Common;
using VibeCrud.Domain.Entities;
using VibeCrud.Domain.Interfaces;

namespace VibeCrud.Application.Services;

public class AddressService : IAddressService
{
    private readonly IAddressRepository _addressRepository;
    private readonly IEventBus _eventBus;

    public AddressService(IAddressRepository addressRepository, IEventBus eventBus)
    {
        _addressRepository = addressRepository;
        _eventBus = eventBus;
    }

    public async Task<AddressDto?> GetByIdAsync(int id)
    {
        var address = await _addressRepository.GetByIdAsync(id);
        return address?.MapToDto();
    }

    public async Task<PagedResult<AddressDto>> GetPagedAsync(AddressQueryDto query)
    {
        var (items, totalCount) = await _addressRepository.GetPagedAsync(
            query.Page, 
            query.PageSize, 
            query.SortBy, 
            query.SortDirection, 
            query.Filter);

        return new PagedResult<AddressDto>
        {
            Items = items.Select(x => x.MapToDto()),
            TotalCount = totalCount,
            PageNumber = query.Page,
            PageSize = query.PageSize
        };
    }

    public async Task<AddressDto> CreateAsync(CreateAddressDto createDto)
    {
        var address = new Address
        {
            FirstName = createDto.FirstName,
            LastName = createDto.LastName,
            Street = createDto.Street,
            HouseNumber = createDto.HouseNumber,
            ZipCode = createDto.ZipCode,
            City = createDto.City,
            Country = createDto.Country,
            Email = createDto.Email,
            Phone = createDto.Phone,
            CreatedAt = DateTime.UtcNow
        };

        var createdAddress = await _addressRepository.CreateAsync(address);
        
        // Publish domain event
        await _eventBus.PublishAsync(new AddressCreatedEvent(createdAddress.Id));
        
        return createdAddress.MapToDto();
    }

    public async Task<AddressDto> UpdateAsync(UpdateAddressDto updateDto)
    {
        var existingAddress = await _addressRepository.GetByIdAsync(updateDto.Id);
        if (existingAddress == null)
            throw new InvalidOperationException($"Address with ID {updateDto.Id} not found");

        existingAddress.FirstName = updateDto.FirstName;
        existingAddress.LastName = updateDto.LastName;
        existingAddress.Street = updateDto.Street;
        existingAddress.HouseNumber = updateDto.HouseNumber;
        existingAddress.ZipCode = updateDto.ZipCode;
        existingAddress.City = updateDto.City;
        existingAddress.Country = updateDto.Country;
        existingAddress.Email = updateDto.Email;
        existingAddress.Phone = updateDto.Phone;
        existingAddress.UpdatedAt = DateTime.UtcNow;

        var updatedAddress = await _addressRepository.UpdateAsync(existingAddress);
        
        // Publish domain event
        await _eventBus.PublishAsync(new AddressUpdatedEvent(updatedAddress.Id));
        
        return updatedAddress.MapToDto();
    }

    public async Task DeleteAsync(int id)
    {
        var address = await _addressRepository.GetByIdAsync(id);
        if (address == null)
            throw new InvalidOperationException($"Address with ID {id} not found");

        await _addressRepository.DeleteAsync(id);
        
        // Publish domain event
        await _eventBus.PublishAsync(new AddressDeletedEvent(id));
    }
}

// Extension methods for mapping
public static class AddressExtensions
{
    public static AddressDto MapToDto(this Address address)
    {
        return new AddressDto
        {
            Id = address.Id,
            FirstName = address.FirstName,
            LastName = address.LastName,
            Street = address.Street,
            HouseNumber = address.HouseNumber,
            ZipCode = address.ZipCode,
            City = address.City,
            Country = address.Country,
            Email = address.Email,
            Phone = address.Phone,
            FullName = address.FullName,
            FullAddress = address.FullAddress,
            CreatedAt = address.CreatedAt,
            UpdatedAt = address.UpdatedAt
        };
    }
}

// Domain events
public record AddressCreatedEvent(int AddressId);
public record AddressUpdatedEvent(int AddressId);
public record AddressDeletedEvent(int AddressId);