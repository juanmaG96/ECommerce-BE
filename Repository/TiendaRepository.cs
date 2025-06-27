using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Models;
using Ecommerce.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repository
{
    public class TiendaRepository : Repository<Tienda>, IRepository<Tienda>, ITiendaRepo
    {
        public TiendaRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Tienda>> GetProductsByTiendaIdAsync(int tiendaId)
        {
            return await Query()
                .Where(t => t.Id == tiendaId)
                .ToListAsync();
        }
    }
}