using Identity.Service.EventHandlers.Responses;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Identity.Service.EventHandlers.Commands
{
    public record UserLoginCommand:IRequest<IdentityAccess>
    {
        [Required,EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
