using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagement.Application.Models;

namespace TaskManagement.Application.Interfaces
{
    public interface IPropertyService
    {
        Task<List<PropertyListItemDto>> SearchAsync(string? name, string? address, decimal? minPrice, decimal? maxPrice);
        Task<PropertyDetailDto?> GetByIdAsync(string propertyId);
        Task<string> CreateAsync(CreatePropertyRequest request);
    }
}


