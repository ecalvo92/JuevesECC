using Microsoft.AspNetCore.Mvc;
using ProyectoWeb_Jueves.Entidades;
using ProyectoWeb_Jueves.Models;
using ProyectoWeb_Jueves.Services;

namespace ProyectoWeb_Jueves.Controllers
{
    [Seguridad]
    [ResponseCache(NoStore = true, Duration = 0)]
    public class UsuarioController(IUsuarioModel _usuarioModel, IUtilitariosModel _utilitariosModel) : Controller
    {
        [HttpGet]
        public IActionResult ConsultarPerfil()
        {
            var resp = _usuarioModel.ConsultarUsuario();
            return View(resp?.Dato);
        }

        [HttpPost]
        public IActionResult ActualizarPerfil(Usuario entidad)
        {
            entidad.Contrasenna = _utilitariosModel.Encrypt(entidad.Contrasenna!);
            var resp = _usuarioModel.ActualizarPerfil(entidad);

            if (resp?.Codigo == "00")
            {
                HttpContext.Session.SetString("Correo", entidad.Correo!);
                HttpContext.Session.SetString("Nombre", entidad.NombreUsuario!);
                return RedirectToAction("ConsultarPerfil", "Usuario");
            }
            else
            {
                ViewBag.MsjPantalla = resp?.Mensaje;
                return View();
            }
        }
    }
}
