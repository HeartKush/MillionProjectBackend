using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Models;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Application.Services
{
    public class OwnerService : IOwnerService
    {
        private readonly IOwnerRepository _ownerRepository;

        public OwnerService(IOwnerRepository ownerRepository)
        {
            _ownerRepository = ownerRepository;
        }

        public async Task<OwnerDetailDto?> GetByIdAsync(string ownerId)
        {
            var owner = await _ownerRepository.GetOwnerByIdAsync(ownerId);
            if (owner == null) return null;

            return new OwnerDetailDto
            {
                IdOwner = owner.IdOwner,
                Name = owner.Name,
                Address = owner.Address,
                Photo = owner.Photo,
                Birthday = owner.Birthday
            };
        }

        public async Task<string> CreateAsync(CreateOwnerRequest request)
        {
            var owner = new Owner
            {
                Name = request.Name,
                Address = request.Address,
                Photo = request.Photo,
                Birthday = request.Birthday
            };

            return await _ownerRepository.CreateOwnerAsync(owner);
        }

        public async Task<List<OwnerListItemDto>> SearchAsync(string? name)
        {
            var owners = await _ownerRepository.SearchOwnersAsync(name);
            var items = new List<OwnerListItemDto>();
            foreach (var owner in owners)
            {
                items.Add(new OwnerListItemDto
                {
                    IdOwner = owner.IdOwner,
                    Name = owner.Name,
                    Photo = owner.Photo
                });
            }
            return items;
        }
    }
}




