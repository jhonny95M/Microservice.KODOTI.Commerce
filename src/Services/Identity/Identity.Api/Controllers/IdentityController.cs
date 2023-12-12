using Identity.Service.EventHandlers.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IMediator mediator;

        public IdentityController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpPost("authentication")]
        public async Task<IActionResult> Authentication([FromBody]UserLoginCommand command)
        {
            if(ModelState.IsValid)
            {
                var result=await mediator.Send(command);
                if(!result.Succeeded)
                    return BadRequest("Acces denied");
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]UserCreateCommand command)
        {
            if(ModelState.IsValid)
            {
                    var result=await mediator.Send(command);
                if(!result.Succeeded) 
                {
                    return BadRequest(result.Errors);
                }
            }
            else
            {
                return BadRequest(ModelState.Values);
            }
            return Ok();
        }
    }
}
