using Microsoft.AspNetCore.Mvc;
using ProyectoWeb_Jueves.Entidades;
using ProyectoWeb_Jueves.Models;
using ProyectoWeb_Jueves.Services;

namespace ProyectoWeb_Jueves.Controllers
{
    [ResponseCache(NoStore = true, Duration = 0)]
    public class HomeController(IUsuarioModel _usuarioModel, IUtilitariosModel _utilitariosModel) : Controller
    {

        [HttpGet]
        public IActionResult IniciarSesion()
        {
            HttpContext.Session.Clear();
            return View();
        }

        [HttpPost]
        public IActionResult IniciarSesion(Usuario entidad)
        {
            entidad.Contrasenna = _utilitariosModel.Encrypt(entidad.Contrasenna!);
            var resp = _usuarioModel.IniciarSesion(entidad);

            if (resp?.Codigo == "00")
            {
                HttpContext.Session.SetString("Correo", resp?.Dato?.Correo!);
                HttpContext.Session.SetString("Nombre", resp?.Dato?.NombreUsuario!);

                if ((bool)(resp?.Dato?.EsTemporal!))
                {
                    return RedirectToAction("CambiarContrasenna", "Home");
                }
                else
                {
                    HttpContext.Session.SetString("Login", "true");
                    return RedirectToAction("PantallaPrincipal", "Home");
                }
            }
            else
            {
                ViewBag.MsjPantalla = resp?.Mensaje;
                return View();
            }
        }


        [HttpGet]
        public IActionResult RegistrarUsuario()
        {
            HttpContext.Session.Clear();
            return View();
        }

        [HttpPost]
        public IActionResult RegistrarUsuario(Usuario entidad)
        {
            entidad.Contrasenna = _utilitariosModel.Encrypt(entidad.Contrasenna!);
            var resp = _usuarioModel.RegistrarUsuario(entidad);

            if (resp?.Codigo == "00")
                return RedirectToAction("IniciarSesion", "Home");
            else
            {
                ViewBag.MsjPantalla = resp?.Mensaje;
                return View();
            }
        }


        [HttpGet]
        public IActionResult RecuperarAcceso()
        {
            HttpContext.Session.Clear();
            return View();
        }

        [HttpPost]
        public IActionResult RecuperarAcceso(Usuario entidad)
        {
            var resp = _usuarioModel.RecuperarAcceso(entidad);

            if (resp?.Codigo == "00")
                return RedirectToAction("IniciarSesion", "Home");
            else
            {
                ViewBag.MsjPantalla = resp?.Mensaje;
                return View();
            }
        }


        [HttpGet]
        public IActionResult CambiarContrasenna()
        {
            var entidad = new Usuario();
            entidad.Correo = HttpContext.Session.GetString("Correo");
            return View(entidad);
        }

        [HttpPost]
        public IActionResult CambiarContrasenna(Usuario entidad)
        {
            if (entidad.Contrasenna?.Trim() == entidad.ContrasennaTemporal?.Trim())
            {
                ViewBag.MsjPantalla = "Debe utilizar una contraseña distinta";
                return View();
            }

            entidad.Contrasenna = _utilitariosModel.Encrypt(entidad.Contrasenna!);
            entidad.ContrasennaTemporal = _utilitariosModel.Encrypt(entidad.ContrasennaTemporal!);
            var resp = _usuarioModel.CambiarContrasenna(entidad);

            if (resp?.Codigo == "00")
            {
                HttpContext.Session.SetString("Login", "true");
                return RedirectToAction("PantallaPrincipal", "Home");
            }
            else
            {
                ViewBag.MsjPantalla = resp?.Mensaje;
                return View();
            }
        }


        [Seguridad]
        [HttpGet]
        public IActionResult PantallaPrincipal()
        {
            return View();
        }


        [Seguridad]
        [HttpGet]
        public IActionResult Salir()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("IniciarSesion","Home");
        }

    }
}