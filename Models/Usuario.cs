using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public long DocumentoIdentidad { get; set; }
        public long Telefono { get; set; }
        [Required]
        public string CorreoElectronico { get; set; }
        public string PasswordHash { get; set; } // Cambiado de Contraseña a PasswordHash
        public RolUsuario Rol { get; set; } // "Admin, Cliente"
        public DateTime FechaRegistro { get; set; } // Auditoría
        public DateTime? UltimoInicioSesion { get; set; } // Auditoría
        public virtual ICollection<Tienda> Tiendas { get; set; } // Relación con tiendas
        public virtual ICollection<Pedido> Pedidos { get; set; } // Relación con pedidos
        public virtual ICollection<Carrito> Carritos { get; set; } // Relación con carritos
        public Usuario()
        {
            Tiendas = new HashSet<Tienda>();
            Pedidos = new HashSet<Pedido>();
            Carritos = new HashSet<Carrito>();
        }
    }

        public enum RolUsuario
    {
        Admin,
        Cliente
    }
}
