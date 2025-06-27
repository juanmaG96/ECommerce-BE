using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Models;
using Ecommerce.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repository
{
    public class PagoRepository : Repository<Pago>, IRepository<Pago>, IPagoRepo
    {
        public PagoRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Pago>> GetPagosByUserIdAsync(int userId)
        {
            return await Query()
                .Where(p => p.Pedido.IdUsuario == userId)
                .ToListAsync();
        }
    }
}