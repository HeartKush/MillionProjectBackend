using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Repositories
{
    public interface IPropertyRepository
    {
        Task<List<Property>> SearchPropertiesAsync(string? name, string? address, decimal? minPrice, decimal? maxPrice);
        Task<Property?> GetPropertyByIdAsync(string propertyId);
        Task<Owner?> GetOwnerByIdAsync(string ownerId);
        Task<PropertyImage?> GetMainImageForPropertyAsync(string propertyId);
        Task<string> CreatePropertyAsync(Property property);
        Task CreatePropertyImageAsync(PropertyImage image);
    }
}


