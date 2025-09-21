using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Infrastructure.Persistence
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly IMongoDatabase _database;

        public OwnerRepository(IMongoDatabase database)
        {
            _database = database;
        }

        private IMongoCollection<Owner> Owners => _database.GetCollection<Owner>("Owners");

        public async Task<Owner?> GetOwnerByIdAsync(string ownerId)
        {
            return await Owners.Find(o => o.IdOwner == ownerId).FirstOrDefaultAsync();
        }

        public async Task<string> CreateOwnerAsync(Owner owner)
        {
            await Owners.InsertOneAsync(owner);
            return owner.IdOwner!;
        }

        public async Task<List<Owner>> SearchOwnersAsync(string? name, string? address = null)
        {
            var filters = new List<MongoDB.Driver.FilterDefinition<Owner>>();

            if (!string.IsNullOrWhiteSpace(name))
            {
                filters.Add(Builders<Owner>.Filter.Regex(o => o.Name, new MongoDB.Bson.BsonRegularExpression(name, "i")));
            }

            if (!string.IsNullOrWhiteSpace(address))
            {
                filters.Add(Builders<Owner>.Filter.Regex(o => o.Address, new MongoDB.Bson.BsonRegularExpression(address, "i")));
            }

            var filter = filters.Count == 0 
                ? Builders<Owner>.Filter.Empty 
                : Builders<Owner>.Filter.And(filters);

            return await Owners.Find(filter).ToListAsync();
        }
    }
}




