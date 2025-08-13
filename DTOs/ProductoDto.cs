using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.DTOs
{
    public class ProductoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int CantidadDisponible { get; set; }
        public int? Descuento { get; set; } // Porcentaje de descuento, puede ser nulo si no hay descuento
        public List<DetallePedidoDto> DetallePedidos { get; set; } // Lista de detalles de pedidos asociados
        public List<string> Imagenes { get; set; } // Lista de URLs de imágenes
        public CategoriaDto Categoria { get; set; } // Información de la categoría del producto
        public TiendaDto Tienda { get; set; } // Información de la tienda a la que pertenece el producto
    }
}