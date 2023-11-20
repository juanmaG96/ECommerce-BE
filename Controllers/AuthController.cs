using Ecommerce.DTOs;
using Ecommerce.Models.Request;
using Ecommerce.Repository.Interfaces;
using Ecommerce.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ecommerce.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioRepo _usuarioRepo;
        private readonly IUsuarioService _usuarioService;

        public AuthController(IUsuarioRepo usuarioRepo, IUsuarioService usuarioService)
        {
            _usuarioRepo = usuarioRepo;
            _usuarioService = usuarioService;
        }

        [HttpPost("login")]
        public IActionResult Autentificar([FromBody] UsuarioLoginRequest uLoginRequest)
        {
            try
            {
                var usuarioResponse = _usuarioService.Auth(uLoginRequest);

                if (usuarioResponse == null)
                    return BadRequest("Usuario o contraseña incorrecta");

                return Ok(usuarioResponse);
            }
            catch(JsonException ex)
            {
                return StatusCode(500, "Error de serialización JSON: " + ex.Message);
            }
            
        }
    }
}
