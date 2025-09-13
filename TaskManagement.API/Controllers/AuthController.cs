using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TaskManagement.Application.Interfaces;

namespace TaskManagement.API.Controllers
{
    /// <summary>
    /// Controlador de autenticación para gestionar la obtención de tokens de acceso.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        /// <summary>
        /// Constructor del controlador de autenticación.
        /// </summary>
        /// <param name="authenticationService">Servicio de autenticación para obtener el token.</param>
        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        /// <summary>
        /// Obtiene un token de acceso para la autenticación. (endpoint de consumo de API externa)
        /// </summary>
        /// <returns>Un token de acceso en formato JSON.</returns>
        /// <response code="200">Devuelve el token de acceso.</response>
        /// <response code="500">Error interno al obtener el token.</response>
        [HttpGet("token")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAuthToken()
        {
            try
            {
                var token = await _authenticationService.GetAccessTokenAsync();
                return Ok(new { AccessToken = token });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al obtener el token de acceso", Error = ex.Message });
            }
        }
    }
}
