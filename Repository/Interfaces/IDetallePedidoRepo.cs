using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Repository.Interfaces
{
    public interface IDetallePedidoRepo
    {
        void Add<DetallePedido>(DetallePedido detallePedido);
        void Update<DetallePedido>(DetallePedido detallePedido);
        void Delete<DetallePedido>(DetallePedido detallePedido);
        Task<IEnumerable<DetallePedido>> GetAllAsync();
        Task<DetallePedido> GetByIdAsync(int id);
        Task<bool> SaveChangesAsync();
    }
}
