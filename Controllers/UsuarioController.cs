using AutoMapper;
using Ecommerce.DTOs;
using Ecommerce.Models;
using Ecommerce.Repository;
using Ecommerce.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepo _uRepository;
        private readonly IMapper _mapper;

        public UsuarioController(IUsuarioRepo uRepository, IMapper mapper)
        {
            _uRepository = uRepository;
            _mapper = mapper;
        }


        // GET: api/<UsuarioController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Obtener todos los usuarios de la base de datos
            var usuarios = await _uRepository.GetAllAsync();

            // Mapear los usuarios a DTOs
            var usuariosDto = usuarios.Select(usuario => _mapper.Map<UsuarioDto>(usuario));

            // Devolver los usuarios DTO
            return Ok(usuariosDto);
        }

        
        // GET api/<UsuarioController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var usuario = await _uRepository.FindByIdAsync(id);

            if (usuario == null)
                return NotFound("Usuario no encontrado");

            var usuarioDto = _mapper.Map<UsuarioDto>(usuario);
            return Ok(usuarioDto);
        }
        

        // POST api/<UsuarioController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UsuarioDto usuarioDto)
        {
            try
            {
                var usuarioNuevo = _mapper.Map<Usuario>(usuarioDto);

                _uRepository.Add(usuarioNuevo);
                await _uRepository.SaveChangesAsync();
                return Ok(usuarioNuevo);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        // PUT api/<UsuarioController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put (int id, [FromBody] UsuarioDto usuarioDto)
        {

            try
            {
            
                if (id != usuarioDto.Id)
                    return BadRequest("Los Ids no coinciden");
            
                var usuario = await _uRepository.FindByIdAsync(id);

                if (usuario == null)
                    return BadRequest();

                //usuarioDto.Id = usuario.Id;

                _mapper.Map(usuarioDto, usuario);

                _uRepository.Update(usuario);
                await _uRepository.SaveChangesAsync();                
                return Ok(usuarioDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        } 

        // DELETE api/<UsuarioController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var usuario = await _uRepository.FindByIdAsync(id);

                if (usuario == null)
                    return NotFound("Usuario no encontrado");

                _uRepository.Delete(usuario);
                await _uRepository.SaveChangesAsync();
                return Ok("Usuario eliminado exitosamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
