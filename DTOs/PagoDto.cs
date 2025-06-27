using System;
using Ecommerce.Models;

namespace Ecommerce.DTOs
{
    public class PagoDto
    {
        public int PedidoId { get; set; }
        public string MercadoPagoPaymentId { get; set; } // ID del pago en MercadoPago
        public decimal Monto { get; set; }
        public string Estado { get; set; } // "Pendiente", "Aprobado", "Rechazado"
        public DateTime FechaCreacion { get; set; }
        public string MetodoPago { get; set; } // Ejemplo: "Tarjeta", "Transferencia"
        public PedidoDto Pedido { get; set; } // Detalles del pedido asociado al pago
    }
}