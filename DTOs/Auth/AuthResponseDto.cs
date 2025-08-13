namespace Ecommerce.DTOs.Auth
{
    public class AuthResponseDto
    {
        public string Token { get; set; }
        public string CorreoElectronico { get; set; }
        public string Rol { get; set; } // "Admin", "Cliente"
    }
}