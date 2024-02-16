using ProyectoWeb_Jueves.Entidades;
using ProyectoWeb_Jueves.Services;

namespace ProyectoWeb_Jueves.Models
{
    public class UsuarioModel : IUsuarioModel
    {
        private readonly HttpClient _http;
        private readonly IConfiguration _configuration;
        public UsuarioModel(HttpClient http, IConfiguration configuration)
        {
            _http = http;
            _configuration = configuration;
        }

        public int RegistrarUsuario(Usuario entidad)
        {
            string url = _configuration.GetSection("settings:UrlApi").Value + "api/Usuario/RegistrarUsuario";
            JsonContent body = JsonContent.Create(entidad);
            var resp = _http.PostAsync(url, body).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<int>().Result;

            return 0;
        }

    }
}
