using System.Collections.Generic;
using System.Threading.Tasks;
using Ecommerce.DTOs;

namespace Ecommerce.Services.Interfaces
{
    public interface IProductoService
    {
        Task<IEnumerable<ProductoDto>> GetProductAsyncByName(string nombre);
        Task<IEnumerable<ProductoDto>> GetAllAsync();
        Task<ProductoDto> GetByIdAsync(int id);
        Task<ProductoDto> AddAsync(ProductoDto productoDto);
        Task<ProductoDto> UpdateAsync(ProductoDto productoDto);
        Task DeleteAsync(ProductoDto productoDto);
        Task DeleteByIdAsync(int id);
    }
}