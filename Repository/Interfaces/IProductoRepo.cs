using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Repository.Interfaces
{
    public interface IProductoRepo
    {
        void Add<Producto>(Producto producto);
        void Update<Producto>(Producto producto);
        void Delete<Producto>(Producto producto);
        Task<IEnumerable<Producto>> GetAllAsync();
        Task<Producto> FindByIdAsync(int id);
        Task<bool> SaveChangesAsync();
    }
}
