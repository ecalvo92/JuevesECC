using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Dapper;
using ProyectoApi_Jueves.Entidades;

namespace ProyectoApi_Jueves.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        [HttpGet]
        public IActionResult ConsultarUsuario()
        {
            using (var context = new SqlConnection("Server=localhost\\MSSQLSERVER01; Database=BD_JUEVES; Trusted_Connection=True;"))
            {
                return Ok(context.Execute("", new { }, commandType: System.Data.CommandType.StoredProcedure));
            }
        }

    }
}
