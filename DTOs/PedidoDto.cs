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
        public string DireccionEnvio { get; set; } // Ejemplo: "Calle Falsa 123, Ciudad, Provincia"
        public string Notas { get; set; } // Notas adicionales del pedido
        public decimal Total { get; set; }
        public UsuarioDto Usuario { get; set; }
        public int? PagoId { get; set; }
        public PagoDto Pago { get; set; }
        public List<DetallePedidoDto> DetallePedidos { get; set; }
    }
}
