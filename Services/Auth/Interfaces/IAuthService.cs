using System.Threading.Tasks;
using Ecommerce.DTOs;
using Ecommerce.DTOs.Auth;

namespace Ecommerce.Services.Auth
{
    public interface IAuthService
    {
        Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
        Task<AuthResponseDto> RegisterAsync(UsuarioDto registerDto);
    }
}