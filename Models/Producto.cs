using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int CantidadDisponible { get; set; }
        public int? Descuento { get; set; } // Porcentaje de descuento
        public ICollection<DetallePedido> DetallePedidos { get; set; }
        public ICollection<Imagen> Imagenes { get; set; }
        public int CategoriaId { get; set; } // Relación con la categoría
        public virtual Categoria Categoria { get; set; } // Navegación a la categoría
        public int TiendaId { get; set; } // Relación con la tienda
        public virtual Tienda Tienda { get; set; } // Navegación a la tienda


        public Producto()
        {
            DetallePedidos = new HashSet<DetallePedido>();
            Imagenes = new HashSet<Imagen>();
        }
    }
}
