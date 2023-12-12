using Catalog.Service.EventHandlers.Commands;
using Catalog.Service.Queries;
using Catalog.Service.Queries.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Service.Common.Collection;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Catalog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> logger;
        private readonly IProductQueryService productQueryService;
        private readonly IMediator mediator;

        public ProductController(ILogger<ProductController> logger, IProductQueryService productQueryService, IMediator mediator)
        {
            this.logger = logger;
            this.productQueryService = productQueryService;
            this.mediator = mediator;
        }

        // GET: api/<ProductController>
        [HttpGet]
        public async Task<DataCollection<ProductDto>> Get(int page=1,int take=10,string? ids=null)
        {
            IEnumerable<int> productIds = null;
            if(!string.IsNullOrEmpty(ids))
                productIds=ids.Split(',').Select(c=>Convert.ToInt32(c));
            return await productQueryService.GetAllAsync(page, take, productIds);
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<ProductDto> Get(int id)
        {
            return await productQueryService.GetByIdAsync(id);
        }

        // POST api/<ProductController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductCreateCommand command)
        {
            await mediator.Publish(command);
            return Ok();

        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
