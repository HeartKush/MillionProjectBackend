using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Models;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Application.Services
{
    public class PropertyTraceService : IPropertyTraceService
    {
        private readonly IPropertyTraceRepository _repository;

        public PropertyTraceService(IPropertyTraceRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> CreateAsync(CreatePropertyTraceRequest request)
        {
            var trace = new PropertyTrace
            {
                DateSale = request.DateSale,
                Name = request.Name,
                Value = request.Value,
                Tax = request.Tax,
                IdProperty = request.IdProperty
            };
            return await _repository.CreatePropertyTraceAsync(trace);
        }

        public async Task<List<PropertyTraceListItemDto>> GetByPropertyIdAsync(string propertyId)
        {
            var traces = await _repository.GetTracesByPropertyIdAsync(propertyId);
            var items = new List<PropertyTraceListItemDto>();
            foreach (var t in traces)
            {
                items.Add(new PropertyTraceListItemDto
                {
                    IdPropertyTrace = t.IdPropertyTrace,
                    DateSale = t.DateSale,
                    Name = t.Name,
                    Value = t.Value,
                    Tax = t.Tax,
                    IdProperty = t.IdProperty
                });
            }
            return items;
        }

        public async Task<PropertyTraceListItemDto?> GetByIdAsync(string traceId)
        {
            var trace = await _repository.GetPropertyTraceByIdAsync(traceId);
            if (trace == null) return null;

            return new PropertyTraceListItemDto
            {
                IdPropertyTrace = trace.IdPropertyTrace,
                DateSale = trace.DateSale,
                Name = trace.Name,
                Value = trace.Value,
                Tax = trace.Tax,
                IdProperty = trace.IdProperty
            };
        }

        public async Task UpdateAsync(string traceId, CreatePropertyTraceRequest request)
        {
            var trace = new PropertyTrace
            {
                IdPropertyTrace = traceId,
                DateSale = request.DateSale,
                Name = request.Name,
                Value = request.Value,
                Tax = request.Tax,
                IdProperty = request.IdProperty
            };
            await _repository.UpdatePropertyTraceAsync(trace);
        }

        public async Task DeleteAsync(string traceId)
        {
            await _repository.DeletePropertyTraceAsync(traceId);
        }
    }
}




