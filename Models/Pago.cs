using System;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class Pago
    {
        [Key]
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public virtual Pedido Pedido { get; set; }
        public string MercadoPagoPaymentId { get; set; } // ID del pago en MercadoPago
        public decimal Monto { get; set; }
        public string Estado { get; set; } // "Pendiente", "Aprobado", "Rechazado"
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaAprobacion { get; set; }
        public string MetodoPago { get; set; } // Ejemplo: "Tarjeta", "Transferencia"
        }
    }