namespace Ecommerce.Config
{
    public class JwtConfig
    {
        public string SecretKey { get; set; } = string.Empty;          // Clave secreta usada para firmar el token
        // public string Issuer { get; set; } = string.Empty;       // Quien emite el token (opcional pero recomendable)
        // public string Audience { get; set; } = string.Empty;     // A quién está destinado el token
        public int ExpireMinutes { get; set; }                   // Tiempo de expiración en minutos
    }
}
