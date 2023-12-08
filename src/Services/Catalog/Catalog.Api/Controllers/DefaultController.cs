using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers
{
    [Route("/")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        private readonly ILogger<DefaultController> logger;

        public DefaultController(ILogger<DefaultController> logger)
        {
            this.logger = logger;
        }
        [HttpGet]
        public string Get() => "Running ...";
    }
}
