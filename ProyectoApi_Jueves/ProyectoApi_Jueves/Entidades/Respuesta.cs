namespace ProyectoApi_Jueves.Entidades
{
    public class Respuesta
    {
        public Respuesta()
        {
            Codigo = "00";
            Mensaje = string.Empty;
        }

        public string Codigo { get; set; }
        public string Mensaje { get; set; }
    }
}
