using Api.Gateway.Models;
using Api.Gateway.Models.Customer.DTOs;
using Api.Gateway.Proxies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Gateway.WebClient.Controllers
{
    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ICustomerProxy customerProxy;

        public ClientController(ICustomerProxy customerProxy)
        {
            this.customerProxy = customerProxy;
        }

        // GET: api/<ClientController>
        [HttpGet]
        public async Task<DataCollection<ClientDto>?> Get(int page,int take)
        {
            return await customerProxy.GetAllAsync(page, take);
        }

        // GET api/<ClientController>/5
        [HttpGet("{id}")]
        public async Task<ClientDto?> Get(int id)
        {
            return await customerProxy.GetByIdAsync(id);
        }
    }
}
