using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoApi_Jueves.Entidades;
using ProyectoApi_Jueves.Services;
using System.Data;
using System.Data.SqlClient;

namespace ProyectoApi_Jueves.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController(IUtilitariosModel _utilitariosModel, IConfiguration _configuration,
                                   IHostEnvironment _hostEnvironment) : ControllerBase
    {
        [AllowAnonymous]
        [Route("IniciarSesion")]
        [HttpPost]
        public IActionResult IniciarSesion(Usuario entidad)
        {
            using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                UsuarioRespuesta respuesta = new UsuarioRespuesta();

                var result = db.Query<Usuario>("IniciarSesion",
                    new { entidad.Correo, entidad.Contrasenna },
                    commandType: CommandType.StoredProcedure).FirstOrDefault();

                if (result == null)
                {
                    respuesta.Codigo = "-1";
                    respuesta.Mensaje = "Sus datos no son correctos.";
                }
                else
                {
                    respuesta.Dato = result;
                    respuesta.Dato.Token = _utilitariosModel.GenerarToken(result.Correo ?? string.Empty);
                }

                return Ok(respuesta);
            }
        }

        [AllowAnonymous]
        [Route("RegistrarUsuario")]
        [HttpPost]
        public IActionResult RegistrarUsuario(Usuario entidad)
        {
            using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                Respuesta respuesta = new Respuesta();

                var result = db.Execute("RegistrarUsuario",
                    new { entidad.Correo, entidad.Contrasenna, entidad.NombreUsuario },
                    commandType: CommandType.StoredProcedure);

                if (result <= 0)
                {
                    respuesta.Codigo = "-1";
                    respuesta.Mensaje = "Su correo ya se encuentra registrado.";
                }

                return Ok(respuesta);
            }
        }

        [AllowAnonymous]
        [Route("RecuperarAcceso")]
        [HttpPost]
        public IActionResult RecuperarAcceso(Usuario entidad)
        {
            using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                UsuarioRespuesta respuesta = new UsuarioRespuesta();

                string NuevaContrasenna = _utilitariosModel.GenerarCodigo();
                string Contrasenna = _utilitariosModel.Encrypt(NuevaContrasenna);
                bool EsTemporal = true;

                var result = db.Query<Usuario>("RecuperarAcceso",
                    new { entidad.Correo, Contrasenna, EsTemporal },
                    commandType: CommandType.StoredProcedure).FirstOrDefault();

                if (result == null)
                {
                    respuesta.Codigo = "-1";
                    respuesta.Mensaje = "Sus datos no son correctos.";
                }
                else
                {
                    string ruta = Path.Combine(_hostEnvironment.ContentRootPath, "FormatoCorreo.html");
                    string htmlBody = System.IO.File.ReadAllText(ruta);
                    htmlBody = htmlBody.Replace("@Nombre@", result.NombreUsuario);
                    htmlBody = htmlBody.Replace("@Contrasenna@", NuevaContrasenna);

                    _utilitariosModel.EnviarCorreo(result.Correo!, "Nuevo Acceso!", htmlBody);
                    respuesta.Dato = result;
                }

                return Ok(respuesta);
            }
        }

        [AllowAnonymous]
        [Route("CambiarContrasenna")]
        [HttpPut]
        public IActionResult CambiarContrasenna(Usuario entidad)
        {
            using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                UsuarioRespuesta respuesta = new UsuarioRespuesta();
                bool EsTemporal = false;

                var result = db.Query<Usuario>("CambiarContrasenna",
                    new { entidad.Correo, entidad.Contrasenna, entidad.ContrasennaTemporal, EsTemporal },
                    commandType: CommandType.StoredProcedure).FirstOrDefault();

                if (result == null)
                {
                    respuesta.Codigo = "-1";
                    respuesta.Mensaje = "Sus datos no son correctos.";
                }
                else
                {
                    respuesta.Dato = result;
                }

                return Ok(respuesta);
            }
        }

    }
}