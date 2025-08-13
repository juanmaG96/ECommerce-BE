using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models
{
    public class Pago
    {
        [Key]
        public int Id { get; set; }
        [Required (ErrorMessage = "El ID del pedido es requerido.")]
        public int PedidoId { get; set; }
        [ForeignKey("PedidoId")]
        public virtual Pedido Pedido { get; set; }
        public string MercadoPagoPaymentId { get; set; } // ID del pago en MercadoPago
        [Column(TypeName = "decimal(18,2)")]
        public decimal Monto { get; set; }
        public string Estado { get; set; } // "Pendiente", "Aprobado", "Rechazado", "Cancelado"
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaAprobacion { get; set; }
        [Required]
        public string MetodoPago { get; set; } // Ejemplo: "Tarjeta", "Transferencia"
        }
    }