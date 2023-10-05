using AutoMapper;
using Ecommerce.DTOs;
using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            CreateMap <DetallePedidoDto, DetallePedido>().ReverseMap();
        }
    }
}
