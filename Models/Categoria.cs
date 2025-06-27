using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; } // Ejemplo: "Camisetas", "Mates"
        public string Descripcion { get; set; }
        public virtual ICollection<Producto> Productos { get; set; }
        public Categoria()
        {
            Productos = new HashSet<Producto>();
        }
    }
}