using ProyectoWeb_Jueves.Entidades;

namespace ProyectoWeb_Jueves.Services
{
    public interface IUsuarioModel
    {
        public Respuesta? RegistrarUsuario(Usuario entidad);

        public UsuarioRespuesta? IniciarSesion(Usuario entidad);
    }
}
