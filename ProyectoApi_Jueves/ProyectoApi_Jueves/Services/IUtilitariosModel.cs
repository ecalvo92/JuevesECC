namespace ProyectoApi_Jueves.Services
{
    public interface IUtilitariosModel
    {
        string Encrypt(string texto);

        string? GenerarToken(string Correo);

        string GenerarCodigo();

        void EnviarCorreo(string Destinatario, string Asunto, string Mensaje);
    }
}