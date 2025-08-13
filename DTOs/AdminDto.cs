using System;
using System.Collections.Generic;

namespace Ecommerce.DTOs
{
    public class AdminDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public long DocumentoIdentidad { get; set; }
        public long Telefono { get; set; }
        public string CorreoElectronico { get; set; }
        public string Password { get; set; }
        public DateTime FechaRegistro { get; set; } // Auditoría
        public List<TiendaDto> Pedidos { get; set; } // Relación con tienda
    }
}