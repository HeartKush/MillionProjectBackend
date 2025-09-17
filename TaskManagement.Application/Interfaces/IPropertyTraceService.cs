using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagement.Application.Models;

namespace TaskManagement.Application.Interfaces
{
    public interface IPropertyTraceService
    {
        Task<string> CreateAsync(CreatePropertyTraceRequest request);
        Task<List<PropertyTraceListItemDto>> GetByPropertyIdAsync(string propertyId);
    }
}




