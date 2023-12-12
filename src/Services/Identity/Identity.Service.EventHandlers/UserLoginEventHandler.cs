using Identity.Domain;
using Identity.Persistence.DataBase;
using Identity.Service.EventHandlers.Commands;
using Identity.Service.EventHandlers.Responses;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identity.Service.EventHandlers;

public sealed class UserLoginEventHandler : IRequestHandler<UserLoginCommand, IdentityAccess>
{
    private readonly SignInManager<ApplicationUser> signInManger;
    private readonly ApplicationDbContext context;
    private readonly IConfiguration configuration;

    public UserLoginEventHandler(SignInManager<ApplicationUser> signInManger, ApplicationDbContext context, IConfiguration configuration)
    {
        this.signInManger = signInManger;
        this.context = context;
        this.configuration = configuration;
    }

    public async Task<IdentityAccess> Handle(UserLoginCommand request, CancellationToken cancellationToken)
    {
        var result = new IdentityAccess();
        var user=await context.Users.SingleAsync(c=>c.Email== request.Email);
        var response=await signInManger.CheckPasswordSignInAsync(user,request.Password,false);
        if (response.Succeeded)
        {
            result.Succeeded = true;
            result.AccessToken = await GenerateTokenAsync(user);
        }
        return result;

    }
    private async Task<string> GenerateTokenAsync(ApplicationUser user)
    {
        var secretKey = configuration.GetValue<string>("SecretKey");
        var key=Encoding.UTF8.GetBytes(secretKey);
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier,user.Id),
            new Claim(ClaimTypes.Email,user.Email),
            new Claim(ClaimTypes.Name,user.FirstName!),
            new Claim (ClaimTypes.Surname,user.LastName!),
        };
        var roles=await context.Roles.Where(x=>x.UserRoles!.Any(y=>y.User!.Id==user.Id)).ToListAsync();
        roles.ForEach(c => claims.Add(new Claim(ClaimTypes.Role, c.Name)));
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject=new ClaimsIdentity(claims),
            Expires=DateTime.UtcNow.AddDays(1),
            SigningCredentials=new SigningCredentials(
                new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature
                )
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var createdToken = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(createdToken);
    }
}
