using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class Pedido
    {
        [Key]
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; }
        public string DireccionEnvio { get; set; } // Ejemplo: "Calle Falsa 123, Ciudad, Provincia"
        public string Notas { get; set; } // Notas adicionales del pedido
        public decimal Total { get; set; }
        public int IdUsuario { get; set; }
        public virtual Usuario Usuario { get; set; }
        public int? PagoId { get; set; }
        public virtual Pago Pago { get; set; }
        public ICollection<DetallePedido> DetallePedidos { get; set; }

        public Pedido()
        {
            DetallePedidos = new HashSet<DetallePedido>();
        }
    }
}
