using ProyectoWeb_Jueves.Entidades;

namespace ProyectoWeb_Jueves.Services
{
    public interface IUsuarioModel
    {
        Respuesta? RegistrarUsuario(Usuario entidad);

        UsuarioRespuesta? IniciarSesion(Usuario entidad);

        UsuarioRespuesta? RecuperarAcceso(Usuario entidad);

        UsuarioRespuesta? CambiarContrasenna(Usuario entidad);

        UsuarioRespuesta? ConsultarUsuario();

        Respuesta? ActualizarPerfil(Usuario entidad);
    }
}
