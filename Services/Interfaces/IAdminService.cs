using System.Collections.Generic;
using System.Threading.Tasks;
using Ecommerce.DTOs;
using Ecommerce.DTOs.Auth;

namespace Ecommerce.Services.Interfaces
{
    public interface IAdminService
    {
        Task<AuthResponseDto> CreateUserAdminAsync(AdminDto adminDto);
    }
}