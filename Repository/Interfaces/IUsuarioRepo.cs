using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Repository.Interfaces
{
    public interface IUsuarioRepo
    {
        void Add<Usuario>(Usuario usuario);
        void Update<Usuario>(Usuario usuario);
        void Delete<Usuario>(Usuario usuario);
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task<Usuario> FindByIdAsync(int id);
        Task<Usuario> FindByCorreoElectronico(string email);
        Task<bool> SaveChangesAsync();

        // public abstract Task<T> GetByNombreAsync(string nombre);


        // Podemos hacer una nueva interface IProductoRepository para agregar el metodo para buscar por nombre de producto
        // y en ese caso ProductoRepository va a heredar IPRoductoRepository y no de IRepository
        // tambien debemos modificar en StartUp en ConfigureServices agregando:
        // services.AddScoped<IRepository, UsuarioRepository>(); y tambien para los demas repo de cada clase
        // services.AddScoped<IProductoRepository, ProductoRepository>();




    }
}
