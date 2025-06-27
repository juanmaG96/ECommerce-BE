using System;
using System.Collections.Generic;
using Ecommerce.Models;

namespace Ecommerce.DTOs
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public long DocumentoIdentidad { get; set; }
        public long Telefono { get; set; }
        public string CorreoElectronico { get; set; }
        public string PasswordHash { get; set; }
        public RolUsuario Rol { get; set; } // "Admin", "Cliente"
        public DateTime FechaRegistro { get; set; } // Auditoría
        public List<TiendaDto> Tiendas { get; set; } // Relación con tiendas
        public List<PedidoDto> Pedidos { get; set; } // Relación con pedidos
        public List<CarritoDto> Carritos { get; set; } // Relación con
    }
}
