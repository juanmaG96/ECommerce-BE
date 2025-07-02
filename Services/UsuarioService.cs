using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.DTOs;
using Ecommerce.Models;
using Ecommerce.Repository;
using Ecommerce.Repository.Interfaces;
using Ecommerce.Services.Interfaces;

namespace Ecommerce.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Usuario> _repository;

        public UsuarioService(IMapper mapper, IRepository<Usuario> repository)
        {
            _mapper = mapper;
            //_usuarioRepo = usuarioRepo;
            _repository = repository;
        }
        public async Task<IEnumerable<UsuarioDto>> GetAllAsync()
        {
            var usuarios = await _repository.GetAllAsync();
            // Mapear los productos a DTOs
            var usuariosDto = _mapper.Map<IEnumerable<UsuarioDto>>(usuarios);
            return usuariosDto;
        }
        public async Task<UsuarioDto> GetByIdAsync(int id)
        {
            var usuario = await _repository.GetByIdAsync(id);
            if (usuario == null)
            {
                return null; // O lanzar una excepción
            }
            var usuarioDto = _mapper.Map<UsuarioDto>(usuario);
            return usuarioDto;
        }
        public async Task<UsuarioDto> AddAsync(UsuarioDto usuarioDto)
        {
            if (usuarioDto == null) throw new ArgumentNullException(nameof(usuarioDto));
            var usuario = _mapper.Map<Usuario>(usuarioDto);
            var addUsuario = await _repository.AddAsync(usuario);
            await _repository.SaveChangesAsync();
            return _mapper.Map<UsuarioDto>(addUsuario);
        }
        public async Task<UsuarioDto> UpdateAsync(UsuarioDto usuarioDto)
        {
            if (usuarioDto == null) throw new ArgumentNullException(nameof(usuarioDto));
            var usuario = _mapper.Map<Usuario>(usuarioDto);
            var updatedUsuario = await _repository.UpdateAsync(usuario);
            await _repository.SaveChangesAsync();
            return _mapper.Map<UsuarioDto>(updatedUsuario);
        }
        public async Task DeleteAsync(UsuarioDto usuarioDto)
        {
            var usuario = _mapper.Map<Usuario>(usuarioDto);
            await _repository.DeleteAsync(usuario);
            await _repository.SaveChangesAsync();
        }
        public async Task DeleteByIdAsync(int id)
        {
            await _repository.DeleteByIdAsync(id);
            await _repository.SaveChangesAsync();
        }
    }
}