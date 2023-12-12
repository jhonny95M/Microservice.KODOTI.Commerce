using Identity.Domain;
using Identity.Persistence.DataBase;
using Identity.Service.EventHandlers.Commands;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Identity.Service.EventHandlers
{
    public class UserCreateEventHandler:IRequestHandler<UserCreateCommand,IdentityResult>
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UserCreateEventHandler(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IdentityResult> Handle(UserCreateCommand request, CancellationToken cancellationToken)
        {
            var entry = new ApplicationUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName=request.Email
            };
            return await userManager.CreateAsync(entry, request.Password);
        }
    }
}