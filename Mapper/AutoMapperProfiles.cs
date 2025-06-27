using AutoMapper;
using Ecommerce.DTOs;
using Ecommerce.Models;
using System;

namespace Ecommerce.Mapper
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UsuarioDto, Usuario>().ReverseMap();
            CreateMap<ProductoDto, Producto>().ReverseMap();
            CreateMap<CarritoDto, Carrito>().ReverseMap();
            CreateMap<PedidoDto, Pedido>().ReverseMap();
            CreateMap<DetallePedidoDto, DetallePedido>().ReverseMap();
            CreateMap<CategoriaDto, Categoria>().ReverseMap();
            CreateMap<ImagenDto, Imagen>().ReverseMap();
            CreateMap<PagoDto, Pago>().ReverseMap();
            CreateMap<TiendaDto, Tienda>().ReverseMap();
        }
    }
}
