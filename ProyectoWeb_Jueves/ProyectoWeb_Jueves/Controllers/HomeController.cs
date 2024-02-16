using Microsoft.AspNetCore.Mvc;
using ProyectoWeb_Jueves.Entidades;
using ProyectoWeb_Jueves.Models;
using ProyectoWeb_Jueves.Services;
using System.Diagnostics;

namespace ProyectoWeb_Jueves.Controllers
{
    [ResponseCache(NoStore = true, Duration = 0)]
    public class HomeController : Controller
    {
        private readonly IUsuarioModel _usuarioModel;
        public HomeController(IUsuarioModel usuarioModel)
        {
            _usuarioModel = usuarioModel;
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

        [HttpPost]
        public IActionResult RegistrarUsuario(Usuario entidad)
        {
            var resp = _usuarioModel.RegistrarUsuario(entidad);

            if (resp > 0)
                return RedirectToAction("IniciarSesion", "Home");

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
