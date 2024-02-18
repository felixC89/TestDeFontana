using Aplicacion.DTOs;
using Dominio.Entities;
using AutoMapper;

namespace CrossCutting.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Ventum, VentaDto>().ReverseMap();
            CreateMap<VentaDetalle, VentaDetalleDto>().ReverseMap();
            CreateMap<Producto, ProductoDto>().ReverseMap();
            CreateMap<Local, LocalDto>().ReverseMap();
            CreateMap<Marca, MarcaDto>().ReverseMap();
        }
    }
}
