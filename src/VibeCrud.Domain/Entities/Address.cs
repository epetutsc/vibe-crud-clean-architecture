using VibeCrud.Domain.Common;

namespace VibeCrud.Domain.Entities;

public class Address : BaseEntity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string HouseNumber { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Phone { get; set; }
    
    public string FullName => $"{FirstName} {LastName}";
    public string FullAddress => $"{Street} {HouseNumber}, {ZipCode} {City}, {Country}";
}