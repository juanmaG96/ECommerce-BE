using System;

namespace Ecommerce.DTOs
{
    public class ImagenDto
    {
        public string Url { get; set; } // URL de la imagen
        public bool EsPrincipal { get; set; } // Indica si es la imagen principal del producto
        public DateTime FechaSubida { get; set; } // Fecha de subida de la imagen
        public ProductoDto Producto { get; set; } // Relación con el producto
    }
}