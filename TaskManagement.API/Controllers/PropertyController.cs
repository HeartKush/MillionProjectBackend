using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Models;

namespace TaskManagement.API.Controllers
{
    /// <summary>
    /// Controlador para gestionar propiedades inmobiliarias.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyService _propertyService;

        /// <summary>
        /// Constructor del controlador de propiedades.
        /// </summary>
        /// <param name="propertyService">Servicio de gestión de propiedades.</param>
        public PropertyController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        /// <summary>
        /// Busca propiedades con filtros opcionales.
        /// </summary>
        /// <param name="name">Nombre de la propiedad a buscar (opcional).</param>
        /// <param name="address">Dirección de la propiedad a buscar (opcional).</param>
        /// <param name="minPrice">Precio mínimo de búsqueda (opcional).</param>
        /// <param name="maxPrice">Precio máximo de búsqueda (opcional).</param>
        /// <returns>Lista de propiedades que coinciden con los criterios de búsqueda.</returns>
        /// <response code="200">Devuelve la lista de propiedades encontradas.</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<PropertyListItemDto>), 200)]
        public async Task<IActionResult> Search([FromQuery] string? name, [FromQuery] string? address, [FromQuery] decimal? minPrice, [FromQuery] decimal? maxPrice)
        {
            var items = await _propertyService.SearchAsync(name, address, minPrice, maxPrice);
            return Ok(items);
        }

        /// <summary>
        /// Obtiene una propiedad por su ID.
        /// </summary>
        /// <param name="id">ID único de la propiedad.</param>
        /// <returns>Detalles de la propiedad solicitada.</returns>
        /// <response code="200">Devuelve los detalles de la propiedad.</response>
        /// <response code="404">No se encontró la propiedad con el ID especificado.</response>
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
        /// Crea una nueva propiedad.
        /// </summary>
        /// <param name="request">Datos de la propiedad a crear.</param>
        /// <returns>ID de la propiedad creada.</returns>
        /// <response code="201">Propiedad creada exitosamente.</response>
        /// <response code="400">Datos de entrada inválidos o campos obligatorios faltantes.</response>
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


