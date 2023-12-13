using Client.WebClient.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace Client.WebClient.Controllers
{
    public class AccountController : Controller
    {
        private readonly string _authenticationUrl;
        private readonly ILogger<AccountController>logger;
        public AccountController(IConfiguration configuration, ILogger<AccountController> logger)
        {
            _authenticationUrl = configuration.GetValue<string>("AuthenticationUrl");
            this.logger = logger;

        }
        [HttpGet]
        public IActionResult Login()
        {
            return Redirect(_authenticationUrl + $"?ReturnBaseUrl={this.Request.Scheme}://{this.Request.Host}/");
        }
        [HttpGet]
        public async Task<IActionResult>Connect(string access_token)
        {
            var token=access_token.Split('.');
            byte[]? base64Content = default;// Convert.FromBase64String(token[1]);
            if (token.Length == 3)
            {
                // Asegurarse de que la longitud sea un múltiplo de 4
                int mod4 = token[1].Length % 4;
                if (mod4 > 0)
                    token[1] += new string('=', 4 - mod4);
                // Intentar decodificar
                try
                {
                    base64Content = Convert.FromBase64String(token[1]);
                    // Continuar con el procesamiento
                }
                catch (FormatException ex)
                {
                    // Manejar el error, ya que la cadena no es válida
                    logger.LogError($"Error al decodificar Base64: {ex.Message}");
                }
            }
            else
            {
                // La cadena no tiene el formato esperado
                logger.LogError("La cadena no tiene el formato esperado.");
            }
            var user = JsonSerializer.Deserialize<AccessTokenUserInformation>(base64Content)!;
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.nameid!),
                new Claim(ClaimTypes.Name,user.unique_name!),
                new Claim(ClaimTypes.Email,user.email !),
                new Claim("access_token",access_token)
            };
            var claimsIdentity=new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IssuedUtc = DateTime.UtcNow.AddHours(10),
            };
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,new ClaimsPrincipal(claimsIdentity),authProperties);
            return Redirect("~/");
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("~/");
        }
    }
}
