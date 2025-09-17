using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Repositories
{
    public interface IOwnerRepository
    {
        Task<Owner?> GetOwnerByIdAsync(string ownerId);
        Task<string> CreateOwnerAsync(Owner owner);
        Task<List<Owner>> SearchOwnersAsync(string? name);
    }
}




