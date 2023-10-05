using Ecommerce.Models;
using Ecommerce.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Repository
{
    public class ProductoRepository: IProductoRepo
    {
        private readonly AplicationDbContext _aplicationDbContext;

        public ProductoRepository(AplicationDbContext aplicationDbContext)
        {
            _aplicationDbContext = aplicationDbContext;
        }

        public void Add<Producto>(Producto producto)
        {
            _aplicationDbContext.Add(producto);
        }

        public void Update<Producto>(Producto producto)
        {
            _aplicationDbContext.Update(producto);
        }

        public void Delete<Producto>(Producto producto)
        {
            _aplicationDbContext.Remove(producto);
        }
        public async Task<IEnumerable<Producto>> GetAllAsync()
        {
            var productos = await _aplicationDbContext.Producto.ToListAsync();
            return productos;
        }

        public async Task<Producto> GetByIdAsync(int id)
        {
            var producto = await _aplicationDbContext.Producto.FirstOrDefaultAsync(u => u.Id == id);
            return producto;
        }

        public async Task<bool> SaveChangesAsync()
        {
            // Guardar los cambios en la base de datos
            return await _aplicationDbContext.SaveChangesAsync() > 0;
        }
    }
}
