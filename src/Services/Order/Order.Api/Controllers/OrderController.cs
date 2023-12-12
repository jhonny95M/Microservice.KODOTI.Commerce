using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Order.Service.EventHandlers.Commands;
using Order.Service.Queries;
using Order.Service.Queries.DTOs;
using Service.Common.Collection;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Order.Api.Controllers
{
    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderQueryService orderQueryService;
        private readonly ILogger<OrderController> logger;
        private readonly IMediator mediator;

        public OrderController(IOrderQueryService orderQueryService, ILogger<OrderController> logger, IMediator mediator)
        {
            this.orderQueryService = orderQueryService;
            this.logger = logger;
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<DataCollection<OrderDto>> Get(int page=1, int take=10)
        {
            return await orderQueryService.GetAllAsync(page, take);
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public async Task<OrderDto> Get(int id)=>
            await orderQueryService.GetByIdAsync(id);

        // POST api/<OrderController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderCreateCommand value)
        {
            await mediator.Publish(value);
            return Ok();
        }

    }
}
