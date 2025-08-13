using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Models;

namespace Ecommerce.DTOs
{
    public class CarritoDto
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; } // ID del usuario al que pertenece el carrito
        public decimal Total { get; set; }
        public List<ProductoDto> Productos { get; set; }

    }
}
