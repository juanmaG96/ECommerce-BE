using System;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.DTOs;
using Ecommerce.DTOs.Auth;
using Ecommerce.Models;
using Ecommerce.Repository.Interfaces;
using Ecommerce.Services.Auth.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IJwtService _jwtService;
        private readonly IPasswordHasher<Usuario> _passwordHasher;
        private readonly IMapper _mapper;
        private readonly IRepository<Usuario> _usuarioRepo;

        // Constructor to inject dependencies
        public AuthService(IRepository<Usuario> usuarioRepo, IMapper mapper, IJwtService jwtService, IPasswordHasher<Usuario> passwordHasher)
        {
            _usuarioRepo = usuarioRepo;
            _mapper = mapper;
            _jwtService = jwtService;
            _passwordHasher = passwordHasher;
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
        {
            if (loginDto == null || string.IsNullOrWhiteSpace(loginDto.CorreoElectronico) || string.IsNullOrWhiteSpace(loginDto.Password))
                throw new ArgumentException("Email y contraseña son requeridos.");

            var usuario = await _usuarioRepo.Query()
                .FirstOrDefaultAsync(u => u.CorreoElectronico == loginDto.CorreoElectronico);

            if (usuario == null || _passwordHasher.VerifyHashedPassword(usuario, usuario.PasswordHash, loginDto.Password) != PasswordVerificationResult.Success)
                throw new UnauthorizedAccessException("Credenciales inválidas.");

            var token = await _jwtService.GenerateTokenAsync(usuario);
            var response = _mapper.Map<AuthResponseDto>(usuario);
            response.Token = token;

            return response;
        }

        public async Task<AuthResponseDto> RegisterAsync(UsuarioDto usuarioDto)
        {
            if (usuarioDto == null || string.IsNullOrWhiteSpace(usuarioDto.CorreoElectronico) || string.IsNullOrWhiteSpace(usuarioDto.Password))
                throw new ArgumentException("Email y contraseña son requeridos.");

            var existingUser = await _usuarioRepo.Query()
                .FirstOrDefaultAsync(u => u.CorreoElectronico == usuarioDto.CorreoElectronico);
            if (existingUser != null)
                throw new InvalidOperationException("El correo electrónico ya está registrado.");

            var usuario = new Usuario
            {
                CorreoElectronico = usuarioDto.CorreoElectronico,
                Nombre = usuarioDto.Nombre,
                Rol = RolUsuario.Cliente // Rol por defecto "cliente"
            };
            usuario.PasswordHash = await _jwtService.HashPasswordAsync(usuarioDto.Password);

            await _usuarioRepo.AddAsync(usuario);
            await _usuarioRepo.SaveChangesAsync();

            var token = await _jwtService.GenerateTokenAsync(usuario);
            var response = _mapper.Map<AuthResponseDto>(usuario);
            response.Token = token;

            return response;
        }
    }
}