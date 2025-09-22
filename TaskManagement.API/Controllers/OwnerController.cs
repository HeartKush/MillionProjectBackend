using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Models;

namespace TaskManagement.API.Controllers
{
    /// <summary>
    /// Controlador para gestionar propietarios de propiedades.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class OwnerController : ControllerBase
    {
        private readonly IOwnerService _service;

        /// <summary>
        /// Constructor del controlador de propietarios.
        /// </summary>
        /// <param name="service">Servicio de gestión de propietarios.</param>
        public OwnerController(IOwnerService service)
        {
            _service = service;
        }

        /// <summary>
        /// Busca propietarios por nombre y/o dirección.
        /// </summary>
        /// <param name="name">Nombre del propietario a buscar (opcional).</param>
        /// <param name="address">Dirección del propietario a buscar (opcional).</param>
        /// <returns>Lista de propietarios que coinciden con el criterio de búsqueda.</returns>
        /// <response code="200">Devuelve la lista de propietarios encontrados.</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<OwnerListItemDto>), 200)]
        public async Task<IActionResult> Search([FromQuery] string? name, [FromQuery] string? address)
        {
            var items = await _service.SearchAsync(name, address);
            return Ok(items);
        }

        /// <summary>
        /// Obtiene un propietario por su ID.
        /// </summary>
        /// <param name="id">ID único del propietario.</param>
        /// <returns>Detalles del propietario solicitado.</returns>
        /// <response code="200">Devuelve los detalles del propietario.</response>
        /// <response code="404">No se encontró el propietario con el ID especificado.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OwnerDetailDto), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(string id)
        {
            var owner = await _service.GetByIdAsync(id);
            if (owner == null) return NotFound();
            return Ok(owner);
        }

        /// <summary>
        /// Crea un nuevo propietario.
        /// </summary>
        /// <param name="request">Datos del propietario a crear.</param>
        /// <returns>ID del propietario creado.</returns>
        /// <response code="201">Propietario creado exitosamente.</response>
        /// <response code="400">Datos de entrada inválidos.</response>
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

        /// <summary>
        /// Actualiza un propietario existente.
        /// </summary>
        /// <param name="id">ID único del propietario a actualizar.</param>
        /// <param name="request">Datos actualizados del propietario.</param>
        /// <returns>Sin contenido.</returns>
        /// <response code="204">Propietario actualizado exitosamente.</response>
        /// <response code="400">Datos de entrada inválidos.</response>
        /// <response code="404">No se encontró el propietario con el ID especificado.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(string id, [FromBody] CreateOwnerRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var success = await _service.UpdateAsync(id, request);
            if (!success) return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Elimina un propietario por su ID.
        /// </summary>
        /// <param name="id">ID único del propietario a eliminar.</param>
        /// <returns>Sin contenido.</returns>
        /// <response code="204">Propietario eliminado exitosamente.</response>
        /// <response code="404">No se encontró el propietario con el ID especificado.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(string id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound();

            return NoContent();
        }
    }
}




