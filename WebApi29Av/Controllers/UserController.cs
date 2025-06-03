using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi29Av.Services.IServices;

namespace WebApi29Av.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUsuarioServices _usuarioServices;

        public UserController(IUsuarioServices usuarioServices)
        {
            _usuarioServices = usuarioServices;
        }


        /// Obtiene todos los usuarios registrados.
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var response = await _usuarioServices.ObtenerUsuarios();
            return Ok(response);
        }

        /// Obtiene un usuario por su ID.
        /// <param name="id">ID del usuario</param>
        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUser(int id)
        {
            return Ok(await _usuarioServices.ById(id));
        }


        /// Crea un nuevo usuario.
        /// <param name="request">Datos del usuario</param>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] UsuarioResponse request)
        {
            return Ok(await _usuarioServices.Crear(request));
        }


        /// Elimina un usuario por su ID.
        /// <param name="id">ID del usuario</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _usuarioServices.DeleteUserAsync(id);
            if (!result)
            {
                return NotFound(new { message = "Usuario no encontrado." });
            }

            return Ok(new { message = "Usuario eliminado correctamente." });
        }


        /// Actualiza los datos de un usuario existente.
        /// <param name="id">ID del usuario a actualizar</param>
        /// <param name="request">Datos actualizados</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UsuarioResponse request)
        {
            var result = await _usuarioServices.Update(id, request);

            if (result == null)
                return NotFound(new { message = "Usuario no encontrado." });

            return Ok(result);
        }
    }
}
