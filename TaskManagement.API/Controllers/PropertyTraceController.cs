using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Models;

namespace TaskManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertyTraceController : ControllerBase
    {
        private readonly IPropertyTraceService _service;

        public PropertyTraceController(IPropertyTraceService service)
        {
            _service = service;
        }

        [HttpGet("by-property/{propertyId}")]
        [ProducesResponseType(typeof(List<PropertyTraceListItemDto>), 200)]
        public async Task<IActionResult> GetByPropertyId(string propertyId)
        {
            var items = await _service.GetByPropertyIdAsync(propertyId);
            return Ok(items);
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] CreatePropertyTraceRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var id = await _service.CreateAsync(request);
            return Created(string.Empty, new { id });
        }
    }
}




