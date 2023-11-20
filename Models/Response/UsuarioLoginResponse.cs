using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models.Response
{
    public class UsuarioLoginResponse
    {
        public string CorreoElectronico { get; set; }
        public string Token { get; set; }
        public string Error { get; internal set; }
    }
}
