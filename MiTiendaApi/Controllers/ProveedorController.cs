using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiTiendaApi.Models.Dtos;
using MiTiendaApi.Models.Inputs;
using MiTiendaApi.Services;

namespace MiTiendaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorController : ControllerBase
    {
        private readonly ProveedorService _provService;
        public ProveedorController(ProveedorService provService)
        {
            _provService = provService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<ProveedorDto>))]
        public async Task<IActionResult> Get()
        {
            try
            {
                var providers = await _provService.GetProvedores();
                return Ok(providers);

            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ProveedorDto))]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var provider = await _provService.GetOneProveedor(id);
                return Ok(provider);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(ProveedorDto))]
        public async Task<IActionResult> Post(ProveedorInput providerInput)
        {
            try
            {
                var provider = await _provService.AddProveedor(providerInput);
                return Ok(provider);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(ProveedorDto))]
        public async Task<IActionResult> Put(int id, ProveedorInput providerInput)
        {
            try
            {
                var providerExists = await _provService.GetOneProveedor(id);
                if(providerExists == null) return NotFound(new {message = "Proveedor no encontrado"});

                var providerAdded = await _provService.EditProveedor(id, providerInput);
                return Ok(providerAdded);

            } catch (Exception ex)
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
                var providerExists = await _provService.GetOneProveedor(id);
                if (providerExists == null) return NotFound(new { message = "Proveedor no encontrado" });

                var providerDeleted = await _provService.DeleteProveedor(id);
                return Ok(providerDeleted);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        } 
    }
}
