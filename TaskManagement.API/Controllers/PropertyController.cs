using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Models;

namespace TaskManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyService _propertyService;

        public PropertyController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        /// <summary>
        /// Busca propiedades por nombre, dirección y rango de precio.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<PropertyListItemDto>), 200)]
        public async Task<IActionResult> Search([FromQuery] string? name, [FromQuery] string? address, [FromQuery] decimal? minPrice, [FromQuery] decimal? maxPrice)
        {
            var items = await _propertyService.SearchAsync(name, address, minPrice, maxPrice);
            return Ok(items);
        }

        /// <summary>
        /// Obtiene el detalle de una propiedad.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PropertyDetailDto), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(string id)
        {
            var detail = await _propertyService.GetByIdAsync(id);
            if (detail == null) return NotFound();
            return Ok(detail);
        }

        /// <summary>
        /// Crea una nueva propiedad con una imagen opcional.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(string), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] CreatePropertyRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.Address))
                return BadRequest("Name y Address son obligatorios.");

            var id = await _propertyService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id }, new { id });
        }
    }
}


