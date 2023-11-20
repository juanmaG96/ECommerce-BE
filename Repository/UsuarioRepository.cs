using Ecommerce.Models;
using Ecommerce.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Repository
{
    public class UsuarioRepository : IUsuarioRepo
    {
        private readonly AplicationDbContext _aplicationDbContext;

        public UsuarioRepository(AplicationDbContext aplicationDbContext)
        {
            _aplicationDbContext = aplicationDbContext;
        }

        public void Add<Usuario>(Usuario usuario)
        {
            _aplicationDbContext.Add(usuario);
        }

        public void Update<Usuario>(Usuario usuario)
        {
            _aplicationDbContext.Update(usuario);
        }

        public void Delete<Usuario>(Usuario usuario)
        {
            _aplicationDbContext.Remove(usuario);
        }
        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            IEnumerable<Usuario> usuarios = await _aplicationDbContext.Usuario.ToListAsync();
            return usuarios;
        }

        public async Task<Usuario> FindByIdAsync(int id)
        {
            var usuario = await _aplicationDbContext.Usuario.FindAsync(id);
            return usuario;
        }

        public async Task<Usuario> FindByCorreoElectronico(string email)
        {
            var usuario = await _aplicationDbContext.Usuario.FirstOrDefaultAsync(u => u.CorreoElectronico == email);
            return usuario;
            
        }

        public async Task<bool> SaveChangesAsync()
        {
            // Guardar los cambios en la base de datos
            return await _aplicationDbContext.SaveChangesAsync() > 0;
        }

    }
}
