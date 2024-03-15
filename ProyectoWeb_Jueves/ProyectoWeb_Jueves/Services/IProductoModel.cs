using ProyectoWeb_Jueves.Entidades;

namespace ProyectoWeb_Jueves.Services
{
    public interface IProductoModel
    {
        ProductoRespuesta? ConsultarProductos();
        CategoriaRespuesta? ConsultarCategorias();
        Respuesta? RegistrarProducto(Producto entidad);
        Respuesta? EliminarProducto(long IdProducto);
    }
}
