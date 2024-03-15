using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ProyectoApi_Jueves.Entidades;
using System.Data.SqlClient;
using System.Data;
using Dapper;

namespace ProyectoApi_Jueves.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController(IConfiguration _configuration) : ControllerBase
    {
        [Authorize]
        [Route("ConsultarProductos")]
        [HttpGet]
        public IActionResult ConsultarProductos()
        {
            using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                ProductoRespuesta respuesta = new ProductoRespuesta();

                var result = db.Query<Producto>("ConsultarProductos",
                    new {  },
                    commandType: CommandType.StoredProcedure).ToList();

                if (result == null)
                {
                    respuesta.Codigo = "-1";
                    respuesta.Mensaje = "No hay productos registrados.";
                }
                else
                {
                    respuesta.Datos = result;
                }

                return Ok(respuesta);
            }
        }

        [Authorize]
        [Route("ConsultarCategorias")]
        [HttpGet]
        public IActionResult ConsultarCategorias()
        {
            using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                CategoriaRespuesta respuesta = new CategoriaRespuesta();

                var result = db.Query<Categoria>("ConsultarCategorias",
                    new { },
                    commandType: CommandType.StoredProcedure).ToList();

                if (result == null)
                {
                    respuesta.Codigo = "-1";
                    respuesta.Mensaje = "No hay categorías registradas.";
                }
                else
                {
                    respuesta.Datos = result;
                }

                return Ok(respuesta);
            }
        }        

        [Authorize]
        [Route("RegistrarProducto")]
        [HttpPost]
        public IActionResult RegistrarProducto(Producto entidad)
        {
            using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                Respuesta respuesta = new Respuesta();

                var result = db.Execute("RegistrarProducto",
                    new { entidad.NombreProducto, entidad.Inventario, entidad.IdCategoria },
                    commandType: CommandType.StoredProcedure);

                if (result <= 0)
                {
                    respuesta.Codigo = "-1";
                    respuesta.Mensaje = "Este producto ya se encuentra registrado.";
                }

                return Ok(respuesta);
            }
        }        

    }
}