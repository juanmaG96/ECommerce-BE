using System;
using System.Threading.Tasks;
using Ecommerce.DTOs;
using Ecommerce.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _prodService;

        public ProductoController(IProductoService prodService)
        {
            _prodService = prodService;
        }

        [HttpGet("{nombre}")]
        public async Task<IActionResult> GetByName(string nombre)
        {
            try
            {
                var productos = await _prodService.GetProductAsyncByName(nombre);
                return Ok(productos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var productos = await _prodService.GetAllAsync();
                return Ok(productos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var producto = await _prodService.GetByIdAsync(id);
                if (producto == null)
                    return NotFound("Producto no encontrado");

                return Ok(producto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ProductoDto productoDto)
        {
            try
            {
                var producto = await _prodService.AddAsync(productoDto);
                return Ok(producto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductoDto productoDto)
        {
            try
            {
                var updateProducto = await _prodService.UpdateAsync(productoDto);
                return Ok(updateProducto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _prodService.DeleteByIdAsync(id);
                return Ok("Producto eliminado exitosamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}