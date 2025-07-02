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
    public class ProductoService : IProductoService
    {
        private readonly IMapper _mapper;
        private readonly IProductoRepo _productoRepo;
        //private readonly IRepository<Producto> _repository;   // No es necesario si se usa IProductoRepo que hereda de IRepository

        public ProductoService(IMapper mapper, IProductoRepo productoRepo, IRepository<Producto> repository)
        {
            _mapper = mapper;
            _productoRepo = productoRepo;
        }
        public async Task<IEnumerable<ProductoDto>> GetProductAsyncByName(string nombre)
        {
            // Obtener productos por nombre
            var productos = await _productoRepo.GetProductAsyncByName(nombre);
            // Mapear los productos a DTOs
            var productosDto = _mapper.Map<IEnumerable<ProductoDto>>(productos);
            return productosDto;
        }
        public async Task<IEnumerable<ProductoDto>> GetAllAsync()
        {
            var productos = await _productoRepo.GetAllAsync();
            // Mapear los productos a DTOs
            var productosDto = _mapper.Map<IEnumerable<ProductoDto>>(productos);
            return productosDto;
        }
        public async Task<ProductoDto> GetByIdAsync(int id)
        {
            var producto = await _productoRepo.GetByIdAsync(id);
            if (producto == null) throw new KeyNotFoundException($"Producto con ID {id} no encontrado.");
            var productoDto = _mapper.Map<ProductoDto>(producto);
            return productoDto;
        }
        public async Task<ProductoDto> AddAsync(ProductoDto productoDto)
        {
            if (productoDto == null) throw new ArgumentNullException(nameof(productoDto));
            var producto = _mapper.Map<Producto>(productoDto);
            var addProducto = await _productoRepo.AddAsync(producto);
            await _productoRepo.SaveChangesAsync();
            return _mapper.Map<ProductoDto>(addProducto);
        }
        public async Task<ProductoDto> UpdateAsync(ProductoDto productoDto)
        {
            if (productoDto == null) throw new ArgumentNullException(nameof(productoDto));
            var producto = _mapper.Map<Producto>(productoDto);
            var updatedProducto = await _productoRepo.UpdateAsync(producto);
            await _productoRepo.SaveChangesAsync();
            return _mapper.Map<ProductoDto>(updatedProducto);
        }
        public async Task DeleteAsync(ProductoDto productoDto)
        {
            var producto = _mapper.Map<Producto>(productoDto);
            await _productoRepo.DeleteAsync(producto);
            await _productoRepo.SaveChangesAsync();
        }
        public async Task DeleteByIdAsync(int id)
        {
            await _productoRepo.DeleteByIdAsync(id);
            await _productoRepo.SaveChangesAsync();
        }
    }
}