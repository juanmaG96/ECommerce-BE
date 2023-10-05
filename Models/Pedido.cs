using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class Pedido
    {
        [Key]
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; }
        public decimal Total { get; set; }
        public int IdUsuario { get; set; }
        public virtual Usuario Usuario { get; set; }
        public ICollection<DetallePedido> DetallePedidos { get; set; }

        public Pedido()
        {
            DetallePedidos = new HashSet<DetallePedido>();
        }
    }
}
