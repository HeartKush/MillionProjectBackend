using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Infrastructure.Persistence
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly IMongoDatabase _database;

        public PropertyRepository(IMongoDatabase database)
        {
            _database = database;
        }

        private IMongoCollection<Property> Properties => _database.GetCollection<Property>("Properties");
        private IMongoCollection<Owner> Owners => _database.GetCollection<Owner>("Owners");
        private IMongoCollection<PropertyImage> PropertyImages => _database.GetCollection<PropertyImage>("PropertyImages");
        private IMongoCollection<PropertyTrace> PropertyTraces => _database.GetCollection<PropertyTrace>("PropertyTraces");

        public async Task<List<Property>> SearchPropertiesAsync(string? name, string? address, decimal? minPrice, decimal? maxPrice, string? idOwner)
        {
            var filterBuilder = Builders<Property>.Filter;
            var filter = filterBuilder.Empty;

            if (!string.IsNullOrWhiteSpace(name))
                filter &= filterBuilder.Regex(p => p.Name, new MongoDB.Bson.BsonRegularExpression(name, "i"));

            if (!string.IsNullOrWhiteSpace(address))
                filter &= filterBuilder.Regex(p => p.Address, new MongoDB.Bson.BsonRegularExpression(address, "i"));

            if (minPrice.HasValue)
                filter &= filterBuilder.Gte(p => p.Price, minPrice.Value);

            if (maxPrice.HasValue)
                filter &= filterBuilder.Lte(p => p.Price, maxPrice.Value);

            if (!string.IsNullOrWhiteSpace(idOwner))
                filter &= filterBuilder.Eq(p => p.IdOwner, idOwner);

            return await Properties.Find(filter).ToListAsync();
        }

        public async Task<Property?> GetPropertyByIdAsync(string propertyId)
        {
            return await Properties.Find(p => p.IdProperty == propertyId).FirstOrDefaultAsync();
        }

        public async Task<Owner?> GetOwnerByIdAsync(string ownerId)
        {
            return await Owners.Find(o => o.IdOwner == ownerId).FirstOrDefaultAsync();
        }

        public async Task<PropertyImage?> GetMainImageForPropertyAsync(string propertyId)
        {
            var filter = Builders<PropertyImage>.Filter.And(
                Builders<PropertyImage>.Filter.Eq(i => i.IdProperty, propertyId),
                Builders<PropertyImage>.Filter.Eq(i => i.Enabled, true)
            );

            return await PropertyImages.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<bool> HasTransactionsAsync(string propertyId)
        {
            var filter = Builders<PropertyTrace>.Filter.Eq(t => t.IdProperty, propertyId);
            var count = await PropertyTraces.CountDocumentsAsync(filter);
            return count > 0;
        }

        public async Task<string> CreatePropertyAsync(Property property)
        {
            await Properties.InsertOneAsync(property);
            return property.IdProperty!;
        }

        public async Task CreatePropertyImageAsync(PropertyImage image)
        {
            await PropertyImages.InsertOneAsync(image);
        }

        public async Task UpdatePropertyImageAsync(PropertyImage image)
        {
            var filter = Builders<PropertyImage>.Filter.And(
                Builders<PropertyImage>.Filter.Eq(i => i.IdProperty, image.IdProperty),
                Builders<PropertyImage>.Filter.Eq(i => i.Enabled, true)
            );

            var updateDisable = Builders<PropertyImage>.Update.Set(i => i.Enabled, false);
            await PropertyImages.UpdateManyAsync(filter, updateDisable);

            // Then, create the new image
            await PropertyImages.InsertOneAsync(image);
        }

        public async Task UpdatePropertyAsync(Property property)
        {
            await Properties.ReplaceOneAsync(p => p.IdProperty == property.IdProperty, property);
        }

        public async Task DeletePropertyAsync(string propertyId)
        {
            await Properties.DeleteOneAsync(p => p.IdProperty == propertyId);
        }
    }
}


