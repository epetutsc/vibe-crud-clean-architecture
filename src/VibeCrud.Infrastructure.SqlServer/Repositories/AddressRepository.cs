using Microsoft.EntityFrameworkCore;
using VibeCrud.Domain.Entities;
using VibeCrud.Domain.Interfaces;
using VibeCrud.Infrastructure.SqlServer.Data;

namespace VibeCrud.Infrastructure.SqlServer.Repositories;

public class AddressRepository : IAddressRepository
{
    private readonly VibeCrudDbContext _context;

    public AddressRepository(VibeCrudDbContext context)
    {
        _context = context;
    }

    public async Task<Address?> GetByIdAsync(int id)
    {
        return await _context.Addresses
            .FirstOrDefaultAsync(a => a.Id == id && !a.IsDeleted);
    }

    public async Task<IEnumerable<Address>> GetAllAsync()
    {
        return await _context.Addresses
            .Where(a => !a.IsDeleted)
            .OrderBy(a => a.LastName)
            .ThenBy(a => a.FirstName)
            .ToListAsync();
    }

    public async Task<Address> CreateAsync(Address address)
    {
        address.CreatedAt = DateTime.UtcNow;
        _context.Addresses.Add(address);
        await _context.SaveChangesAsync();
        return address;
    }

    public async Task<Address> UpdateAsync(Address address)
    {
        address.UpdatedAt = DateTime.UtcNow;
        _context.Addresses.Update(address);
        await _context.SaveChangesAsync();
        return address;
    }

    public async Task DeleteAsync(int id)
    {
        var address = await GetByIdAsync(id);
        if (address != null)
        {
            address.IsDeleted = true;
            address.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }
    }

    public async Task<(IEnumerable<Address> Items, int TotalCount)> GetPagedAsync(
        int pageNumber, 
        int pageSize, 
        string? sortBy = null, 
        string? sortDirection = null, 
        string? filter = null)
    {
        var query = _context.Addresses
            .Where(a => !a.IsDeleted);

        // Apply filtering
        if (!string.IsNullOrEmpty(filter))
        {
            query = query.Where(a => 
                a.FirstName.Contains(filter) ||
                a.LastName.Contains(filter) ||
                a.Street.Contains(filter) ||
                a.City.Contains(filter) ||
                a.ZipCode.Contains(filter) ||
                a.Country.Contains(filter) ||
                (a.Email != null && a.Email.Contains(filter)) ||
                (a.Phone != null && a.Phone.Contains(filter)));
        }

        // Get total count before pagination
        var totalCount = await query.CountAsync();

        // Apply sorting
        if (!string.IsNullOrEmpty(sortBy))
        {
            var isDescending = sortDirection?.ToLower() == "desc";
            
            query = sortBy.ToLower() switch
            {
                "firstname" => isDescending ? query.OrderByDescending(a => a.FirstName) : query.OrderBy(a => a.FirstName),
                "lastname" => isDescending ? query.OrderByDescending(a => a.LastName) : query.OrderBy(a => a.LastName),
                "street" => isDescending ? query.OrderByDescending(a => a.Street) : query.OrderBy(a => a.Street),
                "city" => isDescending ? query.OrderByDescending(a => a.City) : query.OrderBy(a => a.City),
                "zipcode" => isDescending ? query.OrderByDescending(a => a.ZipCode) : query.OrderBy(a => a.ZipCode),
                "country" => isDescending ? query.OrderByDescending(a => a.Country) : query.OrderBy(a => a.Country),
                "email" => isDescending ? query.OrderByDescending(a => a.Email) : query.OrderBy(a => a.Email),
                "phone" => isDescending ? query.OrderByDescending(a => a.Phone) : query.OrderBy(a => a.Phone),
                "createdat" => isDescending ? query.OrderByDescending(a => a.CreatedAt) : query.OrderBy(a => a.CreatedAt),
                _ => query.OrderBy(a => a.LastName).ThenBy(a => a.FirstName)
            };
        }
        else
        {
            query = query.OrderBy(a => a.LastName).ThenBy(a => a.FirstName);
        }

        // Apply pagination
        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (items, totalCount);
    }
}