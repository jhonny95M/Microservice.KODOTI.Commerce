using Api.Gateway.Models;
using Api.Gateway.Models.Order.Commands;
using Api.Gateway.Models.Order.DTOs;
using Api.Gateway.Proxies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Gateway.WebClient.Controllers
{
    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderProxy orderProxy;
        private readonly ICustomerProxy customerProxy;
        private readonly ICatalogHttpProxy catalogHttpProxy;

        public OrderController(IOrderProxy orderProxy, ICustomerProxy customerProxy, ICatalogHttpProxy catalogHttpProxy)
        {
            this.orderProxy = orderProxy;
            this.customerProxy = customerProxy;
            this.catalogHttpProxy = catalogHttpProxy;
        }

        // GET: api/<OrderController>
        [HttpGet]
        public async Task<DataCollection<OrderDto>> Get(int page,int take)
        {
            var result=(await orderProxy.GetAllAsync(page, take))!;
            if (result.HasItems)
            {
                var clientIds=result.Items
                    .Select(c=>c.ClientId)
                    .GroupBy(g=>g)
                    .Select(c=>c.Key)!;
                var clients=await customerProxy.GetAllAsync(page, clientIds.Count(),clientIds);
                foreach (var order in result.Items)
                    order.Client=clients?.Items.Single(c=>c.ClientId==order.ClientId);
            }
            return result;
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public async Task<OrderDto> Get(int id)
        {
            var result = (await orderProxy.GetByIdAsync(id))!;
            result.Client= await customerProxy.GetByIdAsync(result.ClientId);
                var productIds = result.Items
                    .Select(c => c.ProductId)
                    .GroupBy(g => g)
                    .Select(c => c.Key)!;
                var products = await catalogHttpProxy.GetAllAsync(1, productIds.Count(), productIds);
                foreach (var item in result.Items)
                    item.Product = products?.Items.Single(c => c.ProductId == item.ProductId);
            return result;
        }

        // POST api/<OrderController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderCreateCommand command)
        {
            await orderProxy.CreateAsync(command);
            return Ok();
        }
    }
}
