using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MiTiendaApi.Data;
using MiTiendaApi.Models.Dtos;
using MiTiendaApi.Models.Entities;
using MiTiendaApi.Models.Inputs;

namespace MiTiendaApi.Services
{
    public class ProveedorService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        public ProveedorService(DataContext context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ProveedorDto>> GetProvedores()
        {
            var providers = await _context.Proveedores.ToListAsync();
            var provDtos = _mapper.Map<List<ProveedorDto>>(providers);
            return provDtos;
        }

        public async Task<ProveedorDto> GetOneProveedor(int id)
        {
            var provider = await _context.Proveedores.Where(x => x.Id == id).FirstOrDefaultAsync();
            var provDto = _mapper.Map<ProveedorDto>(provider);
            return provDto;
        } 

        public async Task<ProveedorDto> AddProveedor(ProveedorInput providerInput)
        {
            try
            {
                Proveedor provider = _mapper.Map<ProveedorInput, Proveedor>(providerInput);
                
                _context.Add(provider);
                await _context.SaveChangesAsync();

                var provDto = _mapper.Map<ProveedorDto>(provider);
                return provDto;

            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ProveedorDto> EditProveedor(int id, ProveedorInput providerInput)
        {
            try
            {
                Proveedor? entity = _context.Proveedores.Where(x => x.Id == id).FirstOrDefault(); 
                if(entity != null)
                {
                    entity.Nombre = providerInput.Nombre;
                    entity.Direccion = providerInput.Direccion;

                    _context.Update(entity);
                    await _context.SaveChangesAsync();
                }

                var provDto = _mapper.Map<ProveedorDto>(entity);
                return provDto;

            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteProveedor(int id)
        {
            try
            {
                Proveedor? provider = _context.Proveedores.Where(x => x.Id == id).FirstOrDefault();
                if(provider != null)
                {
                    _context.Remove(provider);
                    await _context.SaveChangesAsync();
                    return true;
                }

                return false;
            
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
