namespace ProyectoApi_Jueves.Services
{
    public interface IUtilitariosModel
    {
        public string? GenerarToken(string Correo);

        public string Encrypt(string texto);

        public string Decrypt(string texto);
    }
}