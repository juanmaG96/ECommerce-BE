using System;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.DTOs;
using Ecommerce.DTOs.Auth;
using Ecommerce.Models;
using Ecommerce.Repository.Interfaces;
using Ecommerce.Services.Auth.Interfaces;
using Ecommerce.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Services
{
    public class AdminService : IAdminService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Usuario> _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IJwtService _jwtService;
        public AdminService(IMapper mapper, IRepository<Usuario> repository, IHttpContextAccessor httpContextAccessor, IJwtService jwtService)
        {
            _mapper = mapper;
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
            _jwtService = jwtService;
        }

        public async Task<AuthResponseDto> CreateUserAdminAsync(AdminDto adminDto)
        {

            if (adminDto == null || string.IsNullOrWhiteSpace(adminDto.CorreoElectronico) || string.IsNullOrWhiteSpace(adminDto.Password))
                throw new ArgumentException("Email y contraseña son requeridos.");

            // Verificar que el usuario que hace la solicitud es Admin
            var currentUser = _httpContextAccessor.HttpContext?.User;
            if (currentUser == null || !currentUser.IsInRole(RolUsuario.Admin.ToString()))
                throw new UnauthorizedAccessException("Solo los administradores pueden crear usuarios con roles específicos.");

            var existingUser = await _repository.Query()
                .FirstOrDefaultAsync(u => u.CorreoElectronico == adminDto.CorreoElectronico);
            if (existingUser != null)
                throw new InvalidOperationException("El correo electrónico ya está registrado.");
                
            var usuario = new Usuario
            {
                CorreoElectronico = adminDto.CorreoElectronico,
                Nombre = adminDto.Nombre,
                Rol = RolUsuario.Admin // Asignar el rol especificado (Admin)
            };
            usuario.PasswordHash = await _jwtService.HashPasswordAsync(adminDto.Password);

            await _repository.AddAsync(usuario);
            await _repository.SaveChangesAsync();

            var token = await _jwtService.GenerateTokenAsync(usuario);
            var response = _mapper.Map<AuthResponseDto>(usuario);
            response.Token = token;

            return response;
        }
    }
}