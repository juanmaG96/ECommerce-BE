using System.Collections.Generic;

namespace Ecommerce.DTOs
{
    public class TiendaDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } // Nombre del método de pago
        public string Descripcion { get; set; } // Descripción del método de pago
        public string InstagramUrl { get; set; } // URL del perfil de Instagram
        public string WhatsAppNumero { get; set; } // Número para contacto por WhatsApp
        public UsuarioDto Usuario { get; set; } // Emprendedor dueño de la tienda
        public List<ProductoDto> Productos { get; set; } // Lista de productos en
    }
}