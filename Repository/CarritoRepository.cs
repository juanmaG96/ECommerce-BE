using Ecommerce.Models;
using Ecommerce.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Repository
{
    public class CarritoRepository : ICarritoRepo
    {
        private readonly AplicationDbContext _aplicationDbContext;

        public CarritoRepository(AplicationDbContext aplicationDbContext)
        {
            _aplicationDbContext = aplicationDbContext;
        }

        public void Add<Carrito>(Carrito carrito)
        {
            _aplicationDbContext.Add(carrito);
        }

        public void Update<Carrito>(Carrito carrito)
        {
            _aplicationDbContext.Update(carrito);
        }

        public void Delete<Carrito>(Carrito carrito)
        {
            _aplicationDbContext.Remove(carrito);
        }
        public async Task<IEnumerable<Carrito>> GetAllAsync()
        {
            var carritos = await _aplicationDbContext.Carrito.ToListAsync();
            return carritos;
        }

        public async Task<Carrito> GetByIdAsync(int id)
        {
            var carrito = await _aplicationDbContext.Carrito.FirstOrDefaultAsync(u => u.Id == id);
            return carrito;
        }

        public async Task<bool> SaveChangesAsync()
        {
            // Guardar los cambios en la base de datos
            return await _aplicationDbContext.SaveChangesAsync() > 0;
        }
    }
}