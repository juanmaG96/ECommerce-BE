using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.DTOs
{
    public class DetallePedidoDto
    {
        public int Id { get; set; }
        public int IdPedido { get; set; }
        [Required]
        public int IdProducto { get; set; }
        [Required]
        public int Cantidad { get; set; }
    }
}
