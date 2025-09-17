using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Models;

namespace TaskManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OwnerController : ControllerBase
    {
        private readonly IOwnerService _service;

        public OwnerController(IOwnerService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<OwnerListItemDto>), 200)]
        public async Task<IActionResult> Search([FromQuery] string? name)
        {
            var items = await _service.SearchAsync(name);
            return Ok(items);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OwnerDetailDto), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(string id)
        {
            var owner = await _service.GetByIdAsync(id);
            if (owner == null) return NotFound();
            return Ok(owner);
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] CreateOwnerRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var id = await _service.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id }, new { id });
        }
    }
}




