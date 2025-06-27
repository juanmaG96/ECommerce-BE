
using System;
using System.ComponentModel.DataAnnotations;
using Ecommerce.Models;

namespace Ecommerce.Models
{
    public class Imagen
    {
        [Key]
        public int Id { get; set; }
        public string Url { get; set; } // URL de la imagen (almacenada en un servicio como AWS S3, Cloudinary, o localmente)
        public int ProductoId { get; set; } // Relación con el producto
        public virtual Producto Producto { get; set; }
        public bool EsPrincipal { get; set; } // Indica si es la imagen principal del producto
        public DateTime FechaSubida { get; set; } // Para auditoría
    }
}