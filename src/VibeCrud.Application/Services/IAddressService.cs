using VibeCrud.Application.DTOs;
using VibeCrud.Application.Common;

namespace VibeCrud.Application.Services;

public interface IAddressService
{
    Task<AddressDto?> GetByIdAsync(int id);
    Task<PagedResult<AddressDto>> GetPagedAsync(AddressQueryDto query);
    Task<AddressDto> CreateAsync(CreateAddressDto createDto);
    Task<AddressDto> UpdateAsync(UpdateAddressDto updateDto);
    Task DeleteAsync(int id);
}