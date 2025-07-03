using VibeCrud.Domain.Entities;

namespace VibeCrud.Domain.Interfaces;

public interface IAddressRepository
{
    Task<Address?> GetByIdAsync(int id);
    Task<IEnumerable<Address>> GetAllAsync();
    Task<Address> CreateAsync(Address address);
    Task<Address> UpdateAsync(Address address);
    Task DeleteAsync(int id);
    Task<(IEnumerable<Address> Items, int TotalCount)> GetPagedAsync(
        int pageNumber, 
        int pageSize, 
        string? sortBy = null, 
        string? sortDirection = null, 
        string? filter = null);
}