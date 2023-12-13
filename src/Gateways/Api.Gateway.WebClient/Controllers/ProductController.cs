using Api.Gateway.Models;
using Api.Gateway.Models.Catalog.DTOs;
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
    public class ProductController : ControllerBase
    {
        private readonly ICatalogHttpProxy catalogHttpProxy;

        public ProductController(ICatalogHttpProxy catalogHttpProxy)
        {
            this.catalogHttpProxy = catalogHttpProxy;
        }

        [HttpGet]
        public async Task<DataCollection<ProductDto>?> Get(int page,int take)
        {
            return await catalogHttpProxy.GetAllAsync(page,take);
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<ProductDto?> Get(int id)
        {
            return await catalogHttpProxy.GetAsync(id);
        }

    }
}
