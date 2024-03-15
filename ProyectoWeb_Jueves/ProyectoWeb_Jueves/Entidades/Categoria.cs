namespace ProyectoWeb_Jueves.Entidades
{
    public class Categoria
    {
        public long IdCategoria { get; set; }
        public string? NombreCategoria { get; set; }
    }

    public class CategoriaRespuesta
    {
        public CategoriaRespuesta()
        {
            Codigo = "00";
            Mensaje = string.Empty;
            Dato = null;
            Datos = null;
        }

        public string Codigo { get; set; }
        public string Mensaje { get; set; }
        public Categoria? Dato { get; set; }
        public List<Categoria>? Datos { get; set; }
    }
}
