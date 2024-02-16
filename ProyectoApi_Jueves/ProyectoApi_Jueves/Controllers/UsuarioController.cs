using Microsoft.AspNetCore.Mvc;
using ProyectoApi_Jueves.Entidades;
using Microsoft.AspNetCore.Authorization;
using ProyectoApi_Jueves.Services;
using ProyectoApi_Jueves.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Data.SqlClient;
using Dapper;
using System.Data;

namespace ProyectoApi_Jueves.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUtilitariosModel _utilitariosModel;
        private readonly IConfiguration _configuration;
        public UsuarioController(IUtilitariosModel utilitariosModel, IConfiguration configuration) 
        {
            _utilitariosModel = utilitariosModel;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [Route("IniciarSesion")]
        [HttpPost]
        public IActionResult IniciarSesion(Usuario entidad)
        {
            return Ok();
        }

        [AllowAnonymous]
        [Route("RegistrarUsuario")]
        [HttpPost]
        public IActionResult RegistrarUsuario(Usuario entidad)
        {
            using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return Ok(db.Execute("RegistrarUsuario", 
                    new { entidad.Correo, entidad.Contrasenna, entidad.Nombre }, 
                    commandType : CommandType.StoredProcedure));
            }
        }

    }
}