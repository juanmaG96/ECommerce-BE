using Ecommerce.Models;
using Ecommerce.Models.Request;
using Ecommerce.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<Usuario> FindByCorreoElectronico(string email);

        //Task<UsuarioLoginResponse> Auth(UsuarioLoginRequest usuarioLoginRequest);
        UsuarioLoginResponse Auth(UsuarioLoginRequest usuarioLoginRequest);
    }
}
