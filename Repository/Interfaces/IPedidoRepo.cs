using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Repository.Interfaces
{
    public interface IPedidoRepo
    {
        void Add<Pedido>(Pedido pedido);
        void Update<Pedido>(Pedido usuario);
        void Delete<Pedido>(Pedido usuario);
        Task<IEnumerable<Pedido>> GetAllAsync();
        Task<Pedido> FindByIdAsync(int id);
        Task<bool> SaveChangesAsync();
    }
}
