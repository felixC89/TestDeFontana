using Aplicacion.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiVentas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentasController : Controller
    {
        private readonly IVentasAplicacion _ventas;
        public VentasController(IVentasAplicacion ventas)
        {
            _ventas = ventas;
        }

        [HttpGet("GetVentasPorDias/{dias}")]
        public async Task<IActionResult> GetVentasPorDias(int dias)
        {
            try
            {
                var response = await _ventas.GetVentas(dias);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("GetVentasDetalle")]
        public async Task<IActionResult> GetVentasDetalle()
        {
            try
            {
                var response = await _ventas.GetVentasDetalle();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetMarcas")]
        public async Task<IActionResult> GetMarcas()
        {
            try
            {
                var response = await _ventas.GetMarcas();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetProductos")]
        public async Task<IActionResult> GetProductos()
        {
            try
            {
                var response = await _ventas.GetProductos();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetLocal")]
        public async Task<IActionResult> GetLocal()
        {
            try
            {
                var response = await _ventas.GetLocal();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
