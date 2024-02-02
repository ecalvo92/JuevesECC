using Microsoft.AspNetCore.Mvc;
using ProyectoApi_Jueves.Entidades;
using Microsoft.AspNetCore.Authorization;
using ProyectoApi_Jueves.Services;

namespace ProyectoApi_Jueves.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarios _usuarios;

        public UsuarioController(IUsuarios usuarios)
        {
            _usuarios = usuarios;
        }

        [AllowAnonymous]
        [Route("IniciarSesion")]
        [HttpPost]
        public IActionResult IniciarSesion(Usuario entidad)
        {
            if (entidad.Cedula == "304590415" && entidad.Contrasenna == "secreta")
            {
                var token = _usuarios.GenerarToken(entidad.Cedula);
                return Ok(token);
            }
            
            return NotFound("Su usuario no es válido");
        }

        [AllowAnonymous]
        [Route("ConsultarUsuario")]
        [HttpGet]
        public IActionResult ConsultarUsuario()
        {
            return Ok();
        }

    }
}
