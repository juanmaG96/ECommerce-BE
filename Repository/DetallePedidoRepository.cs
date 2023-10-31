using Ecommerce.Models;
using Ecommerce.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Repository
{
    public class DetallePedidoRepository : IDetallePedidoRepo
    {
        private readonly AplicationDbContext _aplicationDbContext;

        public DetallePedidoRepository(AplicationDbContext aplicationDbContext)
        {
            _aplicationDbContext = aplicationDbContext;
        }

        public void Add<DetallePedido>(DetallePedido detallePedido)
        {
            _aplicationDbContext.Add(detallePedido);
        }

        public void Update<DetallePedido>(DetallePedido detallePedido)
        {
            _aplicationDbContext.Update(detallePedido);
        }

        public void Delete<DetallePedido>(DetallePedido detallePedido)
        {
            _aplicationDbContext.Remove(detallePedido);
        }
        public async Task<IEnumerable<DetallePedido>> GetAllAsync()
        {
            var detallePedido = await _aplicationDbContext.DetallePedido.ToListAsync();
            return detallePedido;
        }

        public async Task<DetallePedido> FindByIdAsync(int id)
        {
            var detallePedido = await _aplicationDbContext.DetallePedido.FindAsync(id);
            return detallePedido;
        }

        public async Task<bool> SaveChangesAsync()
        {
            // Guardar los cambios en la base de datos
            return await _aplicationDbContext.SaveChangesAsync() > 0;
        }
    }
}