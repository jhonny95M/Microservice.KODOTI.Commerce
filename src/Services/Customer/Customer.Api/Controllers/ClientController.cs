using Customer.Service.EventHandlers.Commands;
using Customer.Service.Queries;
using Customer.Service.Queries.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Common.Collection;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Customer.Api.Controllers
{
    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientQueryService clientQueryService;
        private readonly ILogger<ClientController>logger;
        private readonly IMediator mediator;

        public ClientController(IClientQueryService clientQueryService, ILogger<ClientController> logger, IMediator mediator)
        {
            this.clientQueryService = clientQueryService;
            this.logger = logger;
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<DataCollection<ClientDto>> Get(int page=1, int take = 10, string? ids = null)
        {
            IEnumerable<int> clients = null;
            if(!string.IsNullOrWhiteSpace(ids))
                clients=ids.Split(',').Select(c=>Convert.ToInt32(c));
            return await clientQueryService.GetAllAsync(page,take,clients);
        }
        [HttpGet("{id}")]
        public async Task<ClientDto> Get(int id)
        {
            return await clientQueryService.GetByIdAsync(id);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ClientCreateCommand command)
        {
            await mediator.Publish(command);
            return Ok();
        }
    }
}
