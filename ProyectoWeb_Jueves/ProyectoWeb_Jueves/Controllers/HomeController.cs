using Microsoft.AspNetCore.Mvc;
using ProyectoWeb_Jueves.Models;
using System.Diagnostics;

namespace ProyectoWeb_Jueves.Controllers
{
    [ResponseCache(NoStore = true, Duration = 0)]
    public class HomeController : Controller
    {
        public HomeController()
        {

        }

        [HttpGet]
        public IActionResult IniciarSesion()
        {
            HttpContext.Session.Clear();
            return View();
        }

        [HttpGet]
        public IActionResult RegistrarUsuario()
        {
            return View();
        }

        [Seguridad]
        [HttpGet]
        public IActionResult PantallaPrincipal()
        {
            return View();
        }

    }
}
