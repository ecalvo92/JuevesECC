using ProyectoWeb_Jueves.Entidades;

namespace ProyectoWeb_Jueves.Services
{
    public interface IProductoModel
    {
        ProductoRespuesta? ConsultarProductos();
        ProductoRespuesta? ConsultarProducto(long IdProducto);
        CategoriaRespuesta? ConsultarCategorias();
        Respuesta? RegistrarProducto(Producto entidad);
        Respuesta? ActualizarProducto(Producto entidad);
        Respuesta? EliminarProducto(long IdProducto);
    }
}
