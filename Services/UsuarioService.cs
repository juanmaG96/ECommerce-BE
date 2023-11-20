using Ecommerce.Models;
using Ecommerce.Models.Common;
using Ecommerce.Models.Request;
using Ecommerce.Models.Response;
using Ecommerce.Repository.Interfaces;
using Ecommerce.Services.Interfaces;
using Ecommerce.Tools;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services
{
    public class UsuarioService: IUsuarioService
    {
        private readonly IUsuarioRepo _uRepository;
        private readonly AppSettings _appSettings;
        private readonly AplicationDbContext _aplicationDbContext;
        public UsuarioService(IUsuarioRepo usuarioRepo, AplicationDbContext aplicationDbContext, IOptions<AppSettings> appSettings)
        {
            _uRepository = usuarioRepo;
            _appSettings = appSettings.Value;
            _aplicationDbContext = aplicationDbContext;
        }

        public async Task<Usuario> FindByCorreoElectronico(string email)
        {
            var usuario = await _uRepository.FindByCorreoElectronico(email);
            return usuario;
        }

        public UsuarioLoginResponse Auth(UsuarioLoginRequest usuarioLoginRequest)
        {

            UsuarioLoginResponse usuarioLoginResponse = new UsuarioLoginResponse();

            string spassword = Encrypt.GetSHA256(usuarioLoginRequest.Contraseña);  //se utiliza si la contraseña se guarda encriptada y se compara en lugar de d.Contraseña == usuarioLoginRequest.Contraseña
                                                                                   // se compara  d.Contraseña == spassword

            var usuario = _aplicationDbContext.Usuario.Where(d => d.CorreoElectronico == usuarioLoginRequest.CorreoElectronico &&
                                                                d.Contraseña == spassword).FirstOrDefault();

            if (usuario == null)
                return null;

            usuarioLoginResponse.CorreoElectronico = usuarioLoginRequest.CorreoElectronico;
            usuarioLoginResponse.Token = GenerarToken(usuario);

            return usuarioLoginResponse;

        }

        private string GenerarToken(Usuario usuario)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();

                var llave = Encoding.ASCII.GetBytes(_appSettings.Secreto);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(
                        new Claim[]
                        {
                           new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                           new Claim(ClaimTypes.Email, usuario.CorreoElectronico)
                        }
                        ),
                    Expires = DateTime.UtcNow.AddDays(30),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(llave), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch(Exception ex)
            {
                return (ex.Message);
            }
            
        }
    }
}
