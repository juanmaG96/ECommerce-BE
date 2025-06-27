using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Repository.Interfaces
{
    public interface IProductoRepo
    {
        Task<IEnumerable<Producto>> GetProductAsyncByName(string nombre);
    }
}
