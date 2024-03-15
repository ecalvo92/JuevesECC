using NuGet.Common;
using ProyectoWeb_Jueves.Entidades;
using ProyectoWeb_Jueves.Services;
using System.Net.Http.Headers;

namespace ProyectoWeb_Jueves.Models
{
    public class ProductoModel(HttpClient _http, IConfiguration _configuration, 
                               IHttpContextAccessor _sesion) : IProductoModel
    {
        public ProductoRespuesta? ConsultarProductos()
        {
            string url = _configuration.GetSection("settings:UrlApi").Value + "api/Producto/ConsultarProductos";
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _sesion.HttpContext?.Session.GetString("Token"));
            var resp = _http.GetAsync(url).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<ProductoRespuesta>().Result;

            return null;
        }

        public Respuesta? RegistrarProducto(Producto entidad)
        {
            string url = _configuration.GetSection("settings:UrlApi").Value + "api/Producto/RegistrarProducto";
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _sesion.HttpContext?.Session.GetString("Token"));
            JsonContent body = JsonContent.Create(entidad);
            var resp = _http.PostAsync(url, body).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<Respuesta>().Result;

            return null;
        }

        public CategoriaRespuesta? ConsultarCategorias()
        {
            string url = _configuration.GetSection("settings:UrlApi").Value + "api/Producto/ConsultarCategorias";
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _sesion.HttpContext?.Session.GetString("Token"));
            var resp = _http.GetAsync(url).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<CategoriaRespuesta>().Result;

            return null;
        }
    }
}
