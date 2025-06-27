using System.Collections.Generic;
using System.Threading.Tasks;
using Ecommerce.Models;

namespace Ecommerce.Repository.Interfaces
{
    public interface ITiendaRepo
    {
        Task<IEnumerable<Tienda>> GetProductsByTiendaIdAsync(int tiendaId);
    }
}