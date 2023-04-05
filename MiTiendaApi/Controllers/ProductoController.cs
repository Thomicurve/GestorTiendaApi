using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiTiendaApi.Models.Dtos;
using MiTiendaApi.Models.Inputs;
using MiTiendaApi.Services;

namespace MiTiendaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly ProductoService _prodService;
        public ProductoController(ProductoService prodService)
        {
            _prodService = prodService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<ProductoDto>))]
        public async Task<IActionResult> Get()
        {
            try
            {
                var products = await _prodService.GetProducts();
                return Ok(products);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{productId}")]
        [ProducesResponseType(200, Type = typeof(ProductoDto))]
        public async Task<IActionResult> Get(int productId)
        {
            try
            {
                var products = await _prodService.GetOneProduct(productId);
                if (products == null) 
                    return NotFound(new {message= "Producto no encontrado."});
                else
                    return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("provider/{providerId}")]
        [ProducesResponseType(200, Type = typeof(List<ProductoDto>))]
        public async Task<IActionResult> GetByProvider(int providerId)
        {
            try
            {
                var products = await _prodService.GetProductsByProvideer(providerId);
                if (products == null)
                    return NotFound(new { message = "Proveedor no encontrado." });
                else
                    return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(ProductoDto))]
        public async Task<IActionResult> Post([FromBody] ProductoInput prodInput)
        {
            try
            {
                var newProduct = await _prodService.AddProduct(prodInput);
                return Ok(newProduct);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
