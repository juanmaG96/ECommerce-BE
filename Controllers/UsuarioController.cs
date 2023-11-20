using AutoMapper;
using Ecommerce.DTOs;
using Ecommerce.Models;
using Ecommerce.Repository;
using Ecommerce.Repository.Interfaces;
using Ecommerce.Services.Interfaces;
using Ecommerce.Tools;
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
        private readonly IPedidoRepo _peRepository;
        private readonly IProductoRepo _prodRepository;
        private readonly IDetallePedidoRepo _detRepository;
        private readonly IPedidoService _pedidoService;

        public UsuarioController(IUsuarioRepo uRepository, IPedidoRepo peRepository, IProductoRepo prodRepository, IDetallePedidoRepo detRepository,
                                        IMapper mapper,
                                        IPedidoService pedidoService)
        {
            _uRepository = uRepository;
            _peRepository = peRepository;
            _prodRepository = prodRepository;
            _detRepository = detRepository;

            _mapper = mapper;

            _pedidoService = pedidoService;
        }


        // GET: api/<UsuarioController>
        [HttpGet]
        public async Task<IActionResult> GetAllUsuarios()
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
        public async Task<IActionResult> GetUsuario(int id)
        {
            var usuario = await _uRepository.FindByIdAsync(id);

            if (usuario == null)
                return NotFound("Usuario no encontrado");

            var usuarioDto = _mapper.Map<UsuarioDto>(usuario);
            return Ok(usuarioDto);
        }


        // POST api/<UsuarioController>
        [HttpPost]
        public async Task<IActionResult> PostUsuario([FromBody] UsuarioDto usuarioDto)
        {
            try
            {
                usuarioDto.Contraseña = Encrypt.GetSHA256(usuarioDto.Contraseña);   //encripta la contraseña

                var usuarioNuevo = _mapper.Map<Usuario>(usuarioDto);

                _uRepository.Add(usuarioNuevo);
                await _uRepository.SaveChangesAsync();
                return Ok(usuarioNuevo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // PUT api/<UsuarioController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, [FromBody] UsuarioDto usuarioDto)
        {

            try
            {

                if (id != usuarioDto.Id)
                    return BadRequest("Los Ids no coinciden");

                var usuario = await _uRepository.FindByIdAsync(id);

                if (usuario == null)
                    return BadRequest();

                usuarioDto.Contraseña = Encrypt.GetSHA256(usuarioDto.Contraseña);   //encripta la contraseña

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
        public async Task<IActionResult> DeleteUsuario(int id)
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

        [HttpPost("{idU}/Pedido")]
        public async Task<IActionResult> PostPedido(int idU, [FromBody] PedidoDto pedidoDto)
        {
            try
            {
                var usuario = await _uRepository.FindByIdAsync(idU);

                if (usuario == null)
                    return NotFound("Usuario no encontrado");

                var pedidoNuevo = new Pedido
                {
                    Fecha = DateTime.Now,
                    Estado = "Pendiente",
                    Usuario = usuario,
                    IdUsuario = idU,
                    DetallePedidos = new List<DetallePedido>()
                };

                decimal total = 0;

                foreach (var detallePedidoDto in pedidoDto.DetallePedidosDto)
                {
                    var idProd = detallePedidoDto.IdProducto;
                    var producto = await _prodRepository.FindByIdAsync(idProd);

                    if (producto == null)
                        return NotFound("Producto no encontrado");

                    var cantidad = detallePedidoDto.Cantidad;
                    if (cantidad == 0)
                        return BadRequest("Cantidad debe ser mayor que 0");

                    if (cantidad > producto.CantidadDisponible)
                        return BadRequest($"No hay suficiente stock en el producto {producto.Nombre}");

                    var detallePedido = _mapper.Map<DetallePedido>(detallePedidoDto);
                    detallePedido.Producto = producto;

                    total += detallePedido.Cantidad * detallePedido.Producto.Precio;

                    producto.CantidadDisponible -= cantidad;
                    _prodRepository.Update(producto);

                    // Establecer la referencia al pedido en el detalle del pedido
                    detallePedido.Pedido = pedidoNuevo;
                    pedidoNuevo.DetallePedidos.Add(detallePedido);
                }

                pedidoNuevo.Total = total;

                _peRepository.Add(pedidoNuevo);
                await _peRepository.SaveChangesAsync();
                // verificar referencias circulares al devolver el pedido
                return Ok(pedidoNuevo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        // se debe modificar a obtener solo todos los pedidos de un usuario
        [HttpGet("pedido")] 
        public async Task<IActionResult> GetAllPedidos()
        {
            var pedidos = await _peRepository.GetAllAsync();

            var pedidosDto = pedidos.Select(pedido => _mapper.Map<PedidoDto>(pedido));

            return Ok(pedidosDto);
        }

        [HttpGet("{idPedido}/pedido")]
        public async Task<IActionResult> GetPedido(int id)
        {
            var pedido = await _peRepository.FindByIdAsync(id);

            if (pedido == null)
                return NotFound("Pedido no encontrado");

            var pedidoDto = _mapper.Map<PedidoDto>(pedido);
            return Ok(pedidoDto);
        }
    }
}
