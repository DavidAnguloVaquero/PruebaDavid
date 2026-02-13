using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using WebApplication2.Servicios;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IPruebaServices _services;

        public OrderController(IPruebaServices services)
        {
            _services = services;
        }

        //[HttpGet]
        //[Route("Listar")]
        //public async Task<IActionResult> ListarOrdenes()
        //{
        //    var order = await _services.AllUsers();
        //    return Ok(order);
        //}


        //crear Orden

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderRequest request)
        {
            try
            {
                var result = await _services.CreateOrderAsync(request);
                return Created("", result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        //Listar ordenes con pag

        [HttpGet]
        [Route("Listar")]
        public async Task<IActionResult> ListarOrdenes([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _services.GetOrdersPagedAsync(page, pageSize);
            return Ok(result); // 200 OK
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _services.GetOrderByIdAsync(id);
            if (result == null)
                return NotFound(); // 404 si no existe
            return Ok(result); // 200 OK
        }

    }
}
