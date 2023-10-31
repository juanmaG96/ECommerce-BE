using Ecommerce.DTOs;
using Ecommerce.Models;
using Ecommerce.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Services
{
    public class PedidoService: IPedidoService
    {
        private readonly AplicationDbContext _aplicationDbContext;

        public PedidoService(AplicationDbContext aplicationDbContext)
        {
            _aplicationDbContext = aplicationDbContext;
        }
        public decimal CalcularTotalPedido (Pedido pedido)
        {
            decimal total = 0;

            foreach(var detallePedido in pedido.DetallePedidos)
            {
                total += detallePedido.Cantidad * detallePedido.Producto.Precio;
            }

            return total;
        }
    }
}
