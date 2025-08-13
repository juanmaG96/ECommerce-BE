using System.Collections.Generic;

namespace Ecommerce.DTOs
{
    public class CategoriaDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } // Ejemplo: "Camisetas", "Mates"
        public string Descripcion { get; set; } // Descripción de la categoría
        public List<ProductoDto> Productos { get; set; } // Lista de productos en la categoría
    }
}