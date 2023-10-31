using Ecommerce.Models;
using Ecommerce.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Repository
{
    public class PedidoRepository : IPedidoRepo
    {
        private readonly AplicationDbContext _aplicationDbContext;

        public PedidoRepository(AplicationDbContext aplicationDbContext)
        {
            _aplicationDbContext = aplicationDbContext;
        }

        public void Add<Pedido>(Pedido pedido)
        {
            _aplicationDbContext.Add(pedido);
        }

        public void Update<Pedido>(Pedido pedido)
        {
            _aplicationDbContext.Update(pedido);
        }

        public void Delete<Pedido>(Pedido pedido)
        {
            _aplicationDbContext.Remove(pedido);
        }
        public async Task<IEnumerable<Pedido>> GetAllAsync()
        {
            var pedidos = await _aplicationDbContext.Pedido.ToListAsync();
            return pedidos;
        }

        public async Task<Pedido> FindByIdAsync(int id)
        {
            var pedido = await _aplicationDbContext.Pedido.FindAsync(id);
            return pedido;
        }

        public async Task<bool> SaveChangesAsync()
        {
            // Guardar los cambios en la base de datos
            return await _aplicationDbContext.SaveChangesAsync() > 0;
        }
    }
}
