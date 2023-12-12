using Catalog.Service.EventHandlers.Commands;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Catalog.Api.Controllers
{
    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductInStockController : ControllerBase
    {
        private readonly ILogger<ProductInStockController>logger;
        private readonly IMediator mediator;

        public ProductInStockController(ILogger<ProductInStockController> logger, IMediator mediator)
        {
            this.logger = logger;
            this.mediator = mediator;
        }

        // GET: api/<ProductInStockController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ProductInStockController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ProductInStockController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ProductInStockController>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ProductInStockUpdateStokCommand command)
        {
            await mediator.Publish(command);
            return NoContent();
        }

        // DELETE api/<ProductInStockController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
