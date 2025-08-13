using System.Threading.Tasks;
using Ecommerce.Models;

namespace Ecommerce.Services.Auth.Interfaces
{
    public interface IJwtService
    {
        Task<string> GenerateTokenAsync(Usuario usuario, double? expireMinutes = null);
        Task<string> HashPasswordAsync(string password);
        
    }
}