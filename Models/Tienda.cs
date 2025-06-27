using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class Tienda
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; } // Ejemplo: "Camisetas FC", "Mates Arg"
        public string Descripcion { get; set; }
        public string InstagramUrl { get; set; } // URL del perfil de Instagram
        public string WhatsAppNumero { get; set; } // Número para contacto por WhatsApp
        public int UsuarioId { get; set; } // Emprendedor dueño de la tienda
        public virtual Usuario Usuario { get; set; }
        public ICollection<Producto> Productos { get; set; }
        public Tienda()
        {
            Productos = new HashSet<Producto>();
        }
    }
}