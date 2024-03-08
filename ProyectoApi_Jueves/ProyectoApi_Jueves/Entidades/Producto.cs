namespace ProyectoApi_Jueves.Entidades
{
    public class Producto
    {
        public long IdProducto { get; set; }
        public string? NombreProducto { get; set; }
        public int Inventario { get; set; }
        public long IdCategoria { get; set; }
        public string? NombreCategoria { get; set; }
        public int CantidadMinima { get; set; }
    }

    public class ProductoRespuesta
    {
        public ProductoRespuesta()
        {
            Codigo = "00";
            Mensaje = string.Empty;
            Dato = null;
            Datos = null;
        }

        public string Codigo { get; set; }
        public string Mensaje { get; set; }
        public Producto? Dato { get; set; }
        public List<Producto>? Datos { get; set; }
    }
}
