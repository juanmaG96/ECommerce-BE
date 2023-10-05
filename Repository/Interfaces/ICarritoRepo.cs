using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Repository.Interfaces
{
    public interface ICarritoRepo
    {
        void Add<Carrito>(Carrito carrito);
        void Update<Carrito>(Carrito carrito);
        void Delete<Carrito>(Carrito carrito);
        Task<IEnumerable<Carrito>> GetAllAsync();
        Task<Carrito> GetByIdAsync(int id);
        Task<bool> SaveChangesAsync();
    }
}
