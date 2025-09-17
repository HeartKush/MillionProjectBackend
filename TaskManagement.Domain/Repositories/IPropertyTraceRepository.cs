using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Repositories
{
    public interface IPropertyTraceRepository
    {
        Task<string> CreatePropertyTraceAsync(PropertyTrace trace);
        Task<List<PropertyTrace>> GetTracesByPropertyIdAsync(string propertyId);
    }
}




