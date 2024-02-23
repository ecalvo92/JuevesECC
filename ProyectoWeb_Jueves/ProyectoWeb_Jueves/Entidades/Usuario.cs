namespace ProyectoWeb_Jueves.Entidades
{
    public class Usuario
    {
        public long IdUsuario { get; set; }
        public string? Correo { get; set; }
        public string? Contrasenna { get; set; }
        public string? NombreUsuario { get; set; }
        public short IdRol { get; set; }
        public string? NombreRol { get; set; }
        public bool Estado { get; set; }
        public bool? EsTemporal { get; set; }
    }

    public class UsuarioRespuesta
    {
        public UsuarioRespuesta()
        {
            Codigo = "00";
            Mensaje = "";
            Dato = null;
            Datos = null;
        }

        public string Codigo { get; set; }
        public string Mensaje { get; set; }
        public Usuario? Dato { get; set; }
        public List<Usuario>? Datos { get; set; }
    }
}
