using Microsoft.AspNetCore.Mvc;
using ProyectoWeb_Jueves.Entidades;
using ProyectoWeb_Jueves.Models;
using ProyectoWeb_Jueves.Services;

namespace ProyectoWeb_Jueves.Controllers
{
    [Seguridad]
    [ResponseCache(NoStore = true, Duration = 0)]
    public class ProductoController(IProductoModel _productoModel) : Controller
    {
        [HttpGet]
        public IActionResult ConsultarProductos()
        {
            var resp = _productoModel.ConsultarProductos();

            if (resp?.Codigo == "00")
                return View(resp?.Datos);
            else
            {
                ViewBag.MsjPantalla = resp?.Mensaje;
                return View(new List<Producto>());
            }
        }
    }
}