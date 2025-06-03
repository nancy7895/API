using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi29Av.Services.IServices;

namespace WebApi29Av.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolController : ControllerBase
    {
        private readonly IRolServices _rolServices;

        public RolController(IRolServices rolServices)
        {
            _rolServices = rolServices;
        }

        /// Obtiene todos los roles registrados.   
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _rolServices.ObenerRoles();
            return Ok(result);
        }


        /// Obtiene un rol por su ID.
        /// <param name="id">ID del rol</param>
        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _rolServices.ById(id));
        }


        /// Crea un nuevo rol.
        /// <param name="request">Datos del rol</param>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] RolResponse request)
        {
            return Ok(await _rolServices.Crear(request));
        }


        /// Elimina un rol por su ID.
        /// <param name="id">ID del rol</param>
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _rolServices.DeleteAsync(id);
            if (!eliminado)
                return NotFound(new { message = "Rol no encontrado." });

            return Ok(new { message = "Rol eliminado correctamente." });
        }


        /// Actualiza los datos de un rol existente.
        /// <param name="id">ID del rol a actualizar</param>
        /// <param name="request">Datos actualizados</param>
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RolResponse request)
        {
            var result = await _rolServices.Update(id, request);

            if (result == null)
                return NotFound(new { message = "Rol no encontrado." });

            return Ok(result);
        }
    }
}
