using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Models;

namespace TaskManagement.API.Controllers
{
    /// <summary>
    /// Controlador para gestionar el historial de trazas de propiedades.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PropertyTraceController : ControllerBase
    {
        private readonly IPropertyTraceService _service;

        /// <summary>
        /// Constructor del controlador de trazas de propiedades.
        /// </summary>
        /// <param name="service">Servicio de gestión de trazas de propiedades.</param>
        public PropertyTraceController(IPropertyTraceService service)
        {
            _service = service;
        }

        /// <summary>
        /// Obtiene todas las trazas de una propiedad específica.
        /// </summary>
        /// <param name="propertyId">ID de la propiedad para obtener sus trazas.</param>
        /// <returns>Lista de trazas asociadas a la propiedad.</returns>
        /// <response code="200">Devuelve la lista de trazas de la propiedad.</response>
        [HttpGet("by-property/{propertyId}")]
        [ProducesResponseType(typeof(List<PropertyTraceListItemDto>), 200)]
        public async Task<IActionResult> GetByPropertyId(string propertyId)
        {
            var items = await _service.GetByPropertyIdAsync(propertyId);
            return Ok(items);
        }

        /// <summary>
        /// Crea una nueva traza para una propiedad.
        /// </summary>
        /// <param name="request">Datos de la traza a crear.</param>
        /// <returns>ID de la traza creada.</returns>
        /// <response code="201">Trazas creada exitosamente.</response>
        /// <response code="400">Datos de entrada inválidos.</response>
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

        /// <summary>
        /// Obtiene una traza específica por su ID.
        /// </summary>
        /// <param name="id">ID de la traza a obtener.</param>
        /// <returns>Detalles de la traza solicitada.</returns>
        /// <response code="200">Devuelve los detalles de la traza.</response>
        /// <response code="404">No se encontró la traza con el ID especificado.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PropertyTraceListItemDto), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(string id)
        {
            var trace = await _service.GetByIdAsync(id);
            if (trace == null) return NotFound();
            return Ok(trace);
        }

        /// <summary>
        /// Actualiza una traza existente.
        /// </summary>
        /// <param name="id">ID de la traza a actualizar.</param>
        /// <param name="request">Datos actualizados de la traza.</param>
        /// <returns>Resultado de la operación.</returns>
        /// <response code="204">Traza actualizada exitosamente.</response>
        /// <response code="400">Datos de entrada inválidos.</response>
        /// <response code="404">No se encontró la traza con el ID especificado.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(string id, [FromBody] CreatePropertyTraceRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.UpdateAsync(id, request);
            return NoContent();
        }

        /// <summary>
        /// Elimina una traza específica.
        /// </summary>
        /// <param name="id">ID de la traza a eliminar.</param>
        /// <returns>Resultado de la operación.</returns>
        /// <response code="204">Traza eliminada exitosamente.</response>
        /// <response code="404">No se encontró la traza con el ID especificado.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(string id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}




