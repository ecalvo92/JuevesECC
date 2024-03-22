namespace ProyectoApi_Jueves.Services
{
    public interface IUtilitariosModel
    {
        string Encrypt(string texto);

        string Decrypt(string texto);

        string? GenerarToken(long IdUsuario);

        string GenerarCodigo();

        void EnviarCorreo(string Destinatario, string Asunto, string Mensaje);
    }
}