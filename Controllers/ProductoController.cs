using AutoMapper;
using Ecommerce.DTOs;
using Ecommerce.Models;
using Ecommerce.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoRepo _pRepository;
        private readonly IMapper _mapper;

        public ProductoController(IProductoRepo pRepository, IMapper mapper)
        {
            _pRepository = pRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProductos()
        {
            try
            {
                var productos = await _pRepository.GetAllAsync();

                var productosDto = productos.Select(producto => _mapper.Map<ProductoDto>(producto));

                return Ok(productosDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProducto(int id)
        {
            try
            {
                var producto = await _pRepository.FindByIdAsync(id);

                if (producto == null)
                    return NotFound("Producto no encontrado");

                var productoDto = _mapper.Map<ProductoDto>(producto);
                return Ok(productoDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostProducto([FromBody] ProductoDto productoDto)
        {
            try
            {
                var productoNuevo = _mapper.Map<Producto>(productoDto);

                _pRepository.Add(productoNuevo);
                await _pRepository.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(int id, [FromBody] ProductoDto productoDto)
        {
            try
            {
                if (id != productoDto.Id)
                    return BadRequest("Los Ids no coinciden");

                var producto = await _pRepository.FindByIdAsync(id);

                if (producto == null)
                    return BadRequest("Los Ids no coinciden");

                _mapper.Map(productoDto, producto);

                _pRepository.Update(producto);
                await _pRepository.SaveChangesAsync();
                return Ok(productoDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            try
            {
                var producto = await _pRepository.FindByIdAsync(id);

                if (producto == null)
                    return NotFound("Producto no encontrado");

                _pRepository.Delete(producto);
                await _pRepository.SaveChangesAsync();
                return Ok("Producto eliminado exitosamente");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
