using ProyectoWeb_Jueves.Entidades;
using ProyectoWeb_Jueves.Services;

namespace ProyectoWeb_Jueves.Models
{
    public class UsuarioModel(HttpClient _http, IConfiguration _configuration) : IUsuarioModel
    {
        public Respuesta? RegistrarUsuario(Usuario entidad)
        {
            string url = _configuration.GetSection("settings:UrlApi").Value + "api/Usuario/RegistrarUsuario";
            JsonContent body = JsonContent.Create(entidad);
            var resp = _http.PostAsync(url, body).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<Respuesta>().Result;

            return null;
        }

        public UsuarioRespuesta? IniciarSesion(Usuario entidad)
        {
            string url = _configuration.GetSection("settings:UrlApi").Value + "api/Usuario/IniciarSesion";
            JsonContent body = JsonContent.Create(entidad);
            var resp = _http.PostAsync(url, body).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<UsuarioRespuesta>().Result;

            return null;
        }

    }
}
