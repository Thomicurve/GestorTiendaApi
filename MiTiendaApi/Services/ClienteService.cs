using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MiTiendaApi.Data;
using MiTiendaApi.Models.Dtos;
using MiTiendaApi.Models.Entities;
using MiTiendaApi.Models.Inputs;

namespace MiTiendaApi.Services
{
    public class ClienteService
    {
        private readonly DataContext context;
        private readonly IMapper mapper;

        public ClienteService(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<List<ClienteDto>> GetClients()
        {
            var clients = await context.Clientes.ToListAsync();
            var clientsDto = mapper.Map<List<ClienteDto>>(clients);

            return clientsDto;
        }

        public async Task<ClienteDto> GetClientById(int id)
        {
            var client = await context.Clientes.Where(x => x.Id == id).FirstOrDefaultAsync();
            var clientDto = mapper.Map<ClienteDto>(client);

            return clientDto;
        }

        public async Task<ClienteDto> CreateClient(ClienteInput cliente)
        {
            try
            {
                Cliente entity = mapper.Map<ClienteInput, Cliente>(cliente); 
                
                context.Add(entity);
                await context.SaveChangesAsync();

                var dto = mapper.Map<ClienteDto>(entity);
                return dto;

            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<ClienteDto> Put(int id, ClienteInput clientInput)
        {
            try
            {
                Cliente? entity = await context.Clientes.Where(x => x.Id == id).FirstOrDefaultAsync();

                if(entity != null)
                {
                    entity.Nombre = clientInput.Nombre;
                    entity.Apellido = clientInput.Apellido;
                    entity.Direccion = clientInput.Direccion;
                    entity.FechaNacimiento = clientInput.FechaNacimiento;

                    context.Entry(entity).State = EntityState.Modified;
                    await context.SaveChangesAsync();
                }
               
                var dto = mapper.Map<Cliente, ClienteDto>(entity);
                return dto;

            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteClient(int id)
        {
            try
            {
                var client = context.Clientes.Where(x =>x.Id == id).FirstOrDefault();

                if (client == null) return false;
                else
                {
                    context.Clientes.Remove(client);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
