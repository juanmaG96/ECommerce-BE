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
        private readonly IRepository<Producto> _repository;

        public ProductoService(IMapper mapper, IProductoRepo productoRepo, IRepository<Producto> repository)
        {
            _mapper = mapper;
            _productoRepo = productoRepo;
            _repository = repository;
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
            var productos = await _repository.GetAllAsync();
            // Mapear los productos a DTOs
            var productosDto = _mapper.Map<IEnumerable<ProductoDto>>(productos);
            return productosDto;
        }
        public async Task<ProductoDto> GetByIdAsync(int id)
        {
            var producto = await _repository.GetByIdAsync(id);
            if (producto == null)
            {
                return null; // O lanzar una excepción
            }
            var productoDto = _mapper.Map<ProductoDto>(producto);
            return productoDto;
        }
        public async Task<ProductoDto> AddAsync(ProductoDto productoDto)
        {
            if (productoDto == null)
            {
                return null; // O lanzar una excepción
            }
            var producto = _mapper.Map<Producto>(productoDto);
            var addProducto = await _repository.AddAsync(producto);
            return _mapper.Map<ProductoDto>(addProducto);
        }
        public async Task<ProductoDto> UpdateAsync(ProductoDto productoDto)
        {
            if (productoDto == null)
            {
                return null; // O lanzar una excepción
            }
            var producto = _mapper.Map<Producto>(productoDto);
            var updatedProducto = await _repository.UpdateAsync(producto);
            return _mapper.Map<ProductoDto>(updatedProducto);
        }
        public async Task DeleteAsync(ProductoDto productoDto)
        {
            var producto = _mapper.Map<Producto>(productoDto);
            await _repository.DeleteAsync(producto);
        }
        public async Task DeleteByIdAsync(int id)
        {
            await _repository.DeleteByIdAsync(id);
        }
    }
}