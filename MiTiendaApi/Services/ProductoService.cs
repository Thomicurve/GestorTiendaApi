using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MiTiendaApi.Data;
using MiTiendaApi.Models.Dtos;
using MiTiendaApi.Models.Entities;
using MiTiendaApi.Models.Inputs;

namespace MiTiendaApi.Services
{
    public class ProductoService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly ProveedorService _provService;
        public ProductoService(DataContext context, IMapper mapper, ProveedorService provService)
        {
            _context = context;
            _mapper = mapper;
            _provService = provService;
        }

        public async Task<List<ProductoDto>> GetProducts()
        {
            var products = await _context.Productos.ToListAsync();
            var prodDto = _mapper.Map<List<ProductoDto>>(products);
            return prodDto;
        }

        public async Task<List<ProductoDto>> GetProductsByProvideer(int providerId)
        {
            var products = await _context.Productos.Where(x => x.ProveedorId == providerId).ToListAsync();
            var prodDto = _mapper.Map<List<ProductoDto>>(products);
            return prodDto;
        }

        public async Task<ProductoDto> GetOneProduct(int id)
        {
            var product = await _context.Productos.Where(x => x.Id == id).FirstOrDefaultAsync();
            var prodDto = _mapper.Map<ProductoDto>(product);
            return prodDto;
        }

        public async Task<ProductoDto> AddProduct(ProductoInput productoInput)
        {
            try
            {
                ProveedorDto provider = await _provService.GetOneProveedor(productoInput.ProveedorId);
                if (provider != null)
                {
                    Producto entity = _mapper.Map<ProductoInput, Producto>(productoInput);
                    _context.Productos.Add(entity);
                    await _context.SaveChangesAsync();

                    var prodDto = _mapper.Map<ProductoDto>(entity);
                    return prodDto;
                } else
                {
                    throw new Exception("Proveedor inexistente");
                }

                
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ProductoDto> EditProduct(int id, ProductoInput productoInput)
        {
            try
            {
                Producto? entity = _context.Productos.Where(x => x.Id == id).FirstOrDefault();
                ProveedorDto provider = await _provService.GetOneProveedor(productoInput.ProveedorId);

                if (entity != null && provider != null)
                {
                    entity.Nombre = productoInput.Nombre;
                    entity.Precio = productoInput.Precio;
                    entity.ProveedorId = productoInput.ProveedorId;

                    _context.Update(entity);
                    await _context.SaveChangesAsync();
                }

                var productDto = _mapper.Map<Producto, ProductoDto>(entity);
                return productDto;
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteProduct (int id)
        {
            try
            {
                Producto? entity = _context.Productos.Where(x => x.Id == id).FirstOrDefault();
                if(entity != null)
                {
                    _context.Remove(entity);
                    await _context.SaveChangesAsync();
                    return true;
                }

                return false;

            } catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
