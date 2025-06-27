using System.Collections.Generic;
using System.Threading.Tasks;
using Ecommerce.Models;

namespace Ecommerce.Repository.Interfaces
{
    public interface IPagoRepo
    {
        Task<IEnumerable<Pago>> GetPagosByUserIdAsync(int userId);
    }
}