using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.DTOs
{
    public class UsuarioLoginDto
    {
        [Required]
        public string CorreoElectronico { get; set; } 
        [Required]
        public string Contraseña { get; set; }
    }
}
