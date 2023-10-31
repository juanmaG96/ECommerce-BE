using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.DTOs
{
    public class PedidoDto
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; }
        public decimal Total { get; set; }
        public int IdUsuario { get; set; }
        public ICollection<DetallePedidoDto> DetallePedidosDto { get; set; }

        public PedidoDto()
        {
            DetallePedidosDto = new HashSet<DetallePedidoDto>();
        }
    }
}
