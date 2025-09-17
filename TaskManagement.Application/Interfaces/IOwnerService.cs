using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagement.Application.Models;

namespace TaskManagement.Application.Interfaces
{
    public interface IOwnerService
    {
        Task<OwnerDetailDto?> GetByIdAsync(string ownerId);
        Task<string> CreateAsync(CreateOwnerRequest request);
        Task<List<OwnerListItemDto>> SearchAsync(string? name);
    }
}




