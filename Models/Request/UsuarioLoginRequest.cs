using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models.Request
{
    public class UsuarioLoginRequest
    {
        public string CorreoElectronico { get; set; }
        public string Contraseña { get; set; }
    }
}
