using AutoMapper;
using Ecommerce.DTOs;
using Ecommerce.DTOs.Auth;
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

            CreateMap<Usuario, AuthResponseDto>()
                .ForMember(dest => dest.Rol, opt => opt.MapFrom(src => src.Rol.ToString()))
                .ReverseMap()
                .ForMember(dest => dest.Rol, opt => opt.MapFrom(src => Enum.Parse<RolUsuario>(src.Rol)));

            CreateMap<LoginDto, Usuario>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()) // ⚠️ Nunca mapear hash desde el Login
                .ForMember(dest => dest.CorreoElectronico, opt => opt.MapFrom(src => src.CorreoElectronico));
        }
    }
}
