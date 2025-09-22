using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Models;
using TaskManagement.Domain.Repositories;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly IPropertyRepository _propertyRepository;

        public PropertyService(IPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }

        public async Task<List<PropertyListItemDto>> SearchAsync(string? name, string? address, decimal? minPrice, decimal? maxPrice, string? idOwner)
        {
            var properties = await _propertyRepository.SearchPropertiesAsync(name, address, minPrice, maxPrice, idOwner);

            var results = new List<PropertyListItemDto>();
            foreach (var property in properties)
            {
                var image = await _propertyRepository.GetMainImageForPropertyAsync(property.IdProperty!);
                var hasTransactions = await _propertyRepository.HasTransactionsAsync(property.IdProperty!);
                results.Add(new PropertyListItemDto
                {
                    IdProperty = property.IdProperty,
                    IdOwner = property.IdOwner,
                    Name = property.Name,
                    Address = property.Address,
                    Price = property.Price,
                    ImageUrl = image?.File,
                    CreatedAt = property.CreatedAt,
                    HasTransactions = hasTransactions,
                    Featured = property.Featured
                });
            }

            return results;
        }

        public async Task<PropertyDetailDto?> GetByIdAsync(string propertyId)
        {
            var property = await _propertyRepository.GetPropertyByIdAsync(propertyId);
            if (property == null) return null;

            var image = await _propertyRepository.GetMainImageForPropertyAsync(property.IdProperty!);
            return new PropertyDetailDto
            {
                IdProperty = property.IdProperty,
                IdOwner = property.IdOwner,
                Name = property.Name,
                Address = property.Address,
                Price = property.Price,
                CodeInternal = property.CodeInternal,
                Year = property.Year,
                ImageUrl = image?.File,
                CreatedAt = property.CreatedAt,
                UpdatedAt = property.UpdatedAt,
                Featured = property.Featured
            };
        }

        public async Task<string> CreateAsync(CreatePropertyRequest request)
        {
            var property = new Property
            {
                Name = request.Name,
                Address = request.Address,
                Price = request.Price,
                CodeInternal = request.CodeInternal,
                Year = request.Year,
                IdOwner = request.IdOwner,
                Featured = request.Featured,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null
            };

            var id = await _propertyRepository.CreatePropertyAsync(property);

            if (!string.IsNullOrWhiteSpace(request.ImageUrl))
            {
                await _propertyRepository.CreatePropertyImageAsync(new PropertyImage
                {
                    IdProperty = id,
                    File = request.ImageUrl,
                    Enabled = request.ImageEnabled
                });
            }

            return id;
        }

        public async Task<string> UpdateAsync(string id, CreatePropertyRequest request)
        {
            var existingProperty = await _propertyRepository.GetPropertyByIdAsync(id);
            if (existingProperty == null)
                throw new ArgumentException($"Property with ID '{id}' not found.");

            var property = new Property
            {
                IdProperty = id,
                Name = request.Name,
                Address = request.Address,
                Price = request.Price,
                CodeInternal = request.CodeInternal,
                Year = request.Year,
                IdOwner = request.IdOwner,
                Featured = request.Featured,
                CreatedAt = existingProperty.CreatedAt,
                UpdatedAt = DateTime.UtcNow
            };

            await _propertyRepository.UpdatePropertyAsync(property);

            if (!string.IsNullOrWhiteSpace(request.ImageUrl))
            {
                await _propertyRepository.UpdatePropertyImageAsync(new PropertyImage
                {
                    IdProperty = id,
                    File = request.ImageUrl,
                    Enabled = request.ImageEnabled
                });
            }

            return id;
        }

        public async Task DeleteAsync(string id)
        {
            var existingProperty = await _propertyRepository.GetPropertyByIdAsync(id);
            if (existingProperty == null)
                throw new ArgumentException($"Property with ID '{id}' not found.");

            await _propertyRepository.DeletePropertyAsync(id);
        }
    }
}


