using Ecommerce.DTOs;
using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Services.Interfaces
{
    public interface IPedidoService
    {
        decimal CalcularTotalPedido(Pedido pedido);
    }
}
