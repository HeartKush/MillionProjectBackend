using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Infrastructure.Persistence
{
    public class PropertyTraceRepository : IPropertyTraceRepository
    {
        private readonly IMongoDatabase _database;

        public PropertyTraceRepository(IMongoDatabase database)
        {
            _database = database;
        }

        private IMongoCollection<PropertyTrace> PropertyTraces => _database.GetCollection<PropertyTrace>("PropertyTraces");

        public async Task<string> CreatePropertyTraceAsync(PropertyTrace trace)
        {
            await PropertyTraces.InsertOneAsync(trace);
            return trace.IdPropertyTrace!;
        }

        public async Task<List<PropertyTrace>> GetTracesByPropertyIdAsync(string propertyId)
        {
            return await PropertyTraces.Find(t => t.IdProperty == propertyId).ToListAsync();
        }
    }
}




