using Ecommerce.Config;
using Ecommerce.Models;
using Ecommerce.Services.Auth.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services.Auth
{
    public class JwtService : IJwtService
    {
        private readonly JwtConfig _jwtConfig;
        private readonly IPasswordHasher<Usuario> _passwordHasher;

        public JwtService(IOptions<JwtConfig> jwtConfig, IPasswordHasher<Usuario> passwordHasher)
        {
            _jwtConfig = jwtConfig?.Value ?? throw new ArgumentNullException(nameof(jwtConfig));
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
            ValidateJwtConfig();
        }

        public async Task<string> GenerateTokenAsync(Usuario usuario, double? expireMinutes = null)
        {
            if (usuario == null)
                throw new ArgumentNullException(nameof(usuario));
            if (string.IsNullOrWhiteSpace(usuario.CorreoElectronico))
                throw new ArgumentException("El correo electrónico del usuario no puede ser nulo o vacío.", nameof(usuario.CorreoElectronico));
            if (string.IsNullOrWhiteSpace(usuario.Rol.ToString()))
                throw new ArgumentException("El rol del usuario no puede ser nulo o vacío.", nameof(usuario.Rol));
            
            // crear la informacion del usuario para el token
            var claims = new List<Claim>    // Claims que se incluirán en el token
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Email, usuario.CorreoElectronico), // Cambiado a ClaimTypes.Email para consistencia
                new Claim(ClaimTypes.Role, usuario.Rol.ToString())
                // Agregar más claims si es necesario, ej: new Claim(ClaimTypes.GivenName, usuario.Nombre)
            };

            // Crear la clave de seguridad
            var key = Encoding.UTF8.GetBytes(_jwtConfig.SecretKey);
            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
            
            // Crear detealle del token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(expireMinutes ?? _jwtConfig.ExpireMinutes),
                // Issuer = _jwtConfig.Issuer,
                // Audience = _jwtConfig.Audience,
                SigningCredentials = credentials
            };

            try
            {
                // Crear el token JWT
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return await Task.FromResult(tokenHandler.WriteToken(token));
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al generar el token JWT.", ex);
            }
        }

        public async Task<string> HashPasswordAsync(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("La contraseña no puede ser nula o vacía.", nameof(password));

            // Usar IPasswordHasher para hashear contraseñas de forma segura
            var usuario = new Usuario(); // Objeto temporal para hashear
            return await Task.FromResult(_passwordHasher.HashPassword(usuario, password));
        }

        private void ValidateJwtConfig()
        {
            if (string.IsNullOrWhiteSpace(_jwtConfig.SecretKey) || _jwtConfig.SecretKey.Length < 32)
                throw new InvalidOperationException("La clave JWT debe tener al menos 32 caracteres.");
            //if (string.IsNullOrWhiteSpace(_jwtConfig.Issuer))
                //throw new InvalidOperationException("El emisor JWT no puede ser nulo o vacío.");
            //if (string.IsNullOrWhiteSpace(_jwtConfig.Audience))
                //throw new InvalidOperationException("La audiencia JWT no puede ser nula o vacía.");
            if (_jwtConfig.ExpireMinutes <= 0)
                throw new InvalidOperationException("El tiempo de expiración debe ser mayor a 0 minutos.");
        }
    }
}