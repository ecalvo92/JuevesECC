using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        [HttpGet]
        public IActionResult AgregarProducto()
        {
            CargarCategorias();
            return View();
        }

        [HttpPost]
        public IActionResult AgregarProducto(Producto entidad)
        {
            var resp = _productoModel.RegistrarProducto(entidad);

            if (resp?.Codigo == "00")
                return RedirectToAction("ConsultarProductos", "Producto");
            else
            {
                CargarCategorias();
                ViewBag.MsjPantalla = resp?.Mensaje;
                return View();
            }
        }

        [HttpPost]
        public IActionResult EliminarProducto(Producto entidad)
        {
            var resp = _productoModel.EliminarProducto(entidad.IdProducto);

            if (resp?.Codigo == "00")
                return RedirectToAction("ConsultarProductos", "Producto");
            else
            {
                ViewBag.MsjPantalla = resp?.Mensaje;
                return View();
            }
        }

        [HttpGet]
        public IActionResult ActualizarProducto(long id)
        {
            var resp = _productoModel.ConsultarProducto(id);
            CargarCategorias();
            return View(resp?.Dato);
        }

        [HttpPost]
        public IActionResult ActualizarProducto(Producto entidad)
        {
            var resp = _productoModel.ActualizarProducto(entidad);

            if (resp?.Codigo == "00")
                return RedirectToAction("ConsultarProductos", "Producto");
            else
            {
                CargarCategorias();
                ViewBag.MsjPantalla = resp?.Mensaje;
                return View();
            }
        }

        private void CargarCategorias()
        {
            var lista = new List<SelectListItem> { new SelectListItem { Value = string.Empty, Text = "Seleccione..." } };

            foreach (var item in _productoModel.ConsultarCategorias()?.Datos!)
                lista.Add(new SelectListItem { Value = item.IdCategoria.ToString(), Text = item.NombreCategoria });

            ViewBag.Categorias = lista;
        }

    }
}