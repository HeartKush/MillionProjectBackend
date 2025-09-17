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

        public async Task<List<Owner>> SearchOwnersAsync(string? name)
        {
            var filter = string.IsNullOrWhiteSpace(name)
                ? Builders<Owner>.Filter.Empty
                : Builders<Owner>.Filter.Regex(o => o.Name, new MongoDB.Bson.BsonRegularExpression(name, "i"));
            return await Owners.Find(filter).ToListAsync();
        }
    }
}




