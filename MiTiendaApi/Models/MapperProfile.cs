using AutoMapper;
using MiTiendaApi.Models.Dtos;
using MiTiendaApi.Models.Entities;
using MiTiendaApi.Models.Inputs;

namespace MiTiendaApi.Models
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Cliente, ClienteDto>();
            CreateMap<Producto, ProductoDto>();
            CreateMap<Proveedor, ProveedorDto>();
            CreateMap<ClienteInput, Cliente>();
            CreateMap<ProveedorInput, Proveedor>();
            CreateMap<ProductoInput, Producto>();
        }
    }
}
