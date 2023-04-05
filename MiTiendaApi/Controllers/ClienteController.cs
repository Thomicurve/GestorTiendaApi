using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiTiendaApi.Models.Dtos;
using MiTiendaApi.Models.Inputs;
using MiTiendaApi.Services;

namespace MiTiendaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteService _clientService;
        public ClienteController(ClienteService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<ClienteDto>))]
        public async Task<IActionResult> Get()
        {
            try
            {
                var clients = await _clientService.GetClients();
                return Ok(clients);

            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ClienteDto))]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var client = await _clientService.GetClientById(id);

                if (client == null)
                    return NotFound(new {message = "Cliente no encontrado"});
                else
                    return Ok(client);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(ClienteDto))]
        public async Task<IActionResult> Post([FromBody] ClienteInput client)
        {
            try
            {
                var newClient = await _clientService.CreateClient(client);
                return Ok(newClient);
            } catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(ClienteDto))]
        public async Task<IActionResult> Put(int id, [FromBody]ClienteInput clientInput)
        {
            try
            {
                var clientToUpdate = await _clientService.GetClientById(id);
                if (clientToUpdate == null) return NotFound();

                var clientAdded = await _clientService.Put(id, clientInput);
                return Ok(clientAdded);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        } 

        [HttpDelete("{id}")]
        [ProducesResponseType(200, Type = typeof(bool))]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var clientDeleted = await _clientService.DeleteClient(id);
                if (clientDeleted)
                    return Ok(new { message = "Cliente eliminado." });
                else
                    return BadRequest(new {message = "Error al eliminar el cliente."});

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
