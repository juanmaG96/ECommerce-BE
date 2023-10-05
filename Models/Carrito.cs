using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class Carrito
    {
        [Key]
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public decimal Total { get; set; }
        public ICollection<Producto> Productos { get; set; }

        public Carrito()
        {
            Productos = new HashSet<Producto>();
        }

    }
}
