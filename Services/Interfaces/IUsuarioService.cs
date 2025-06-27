using System.Collections.Generic;
using System.Threading.Tasks;
using Ecommerce.DTOs;

namespace Ecommerce.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<IEnumerable<UsuarioDto>> GetAllAsync();
        Task<UsuarioDto> GetByIdAsync(int id);
        Task<UsuarioDto> AddAsync(UsuarioDto usuarioDto);
        Task<UsuarioDto> UpdateAsync(UsuarioDto usuarioDto);
        Task DeleteAsync(UsuarioDto usuarioDto);
        Task DeleteByIdAsync(int id);
    }
}