using Api.Gateway.Models;
using Api.Gateway.Models.Catalog.DTOs;
using Api.Gateway.Models.Customer.DTOs;
using Api.Gateway.Models.Order.Commands;
using Api.Gateway.WebClient.Proxy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Client.WebClient.Pages.Orders;

//[Authorize(AuthenticationSchemes =CookieAuthenticationDefaults.AuthenticationScheme)]
public class CreateModel : PageModel
{
    private readonly ILogger<CreateModel> _logger;
    private readonly IOrderProxy _orderProxy;
    private readonly IClientProxy _clientProxy;
    private readonly IProductProxy _productProxy;

    public CreateModel(ILogger<CreateModel> logger, IOrderProxy orderProxy, IClientProxy clientProxy, IProductProxy productProxy)
    {
        _logger = logger;
        _orderProxy = orderProxy;
        _clientProxy = clientProxy;
        _productProxy = productProxy;
    }

    public DataCollection<ProductDto> Products { get; set; }=new DataCollection<ProductDto>();
    public DataCollection<ClientDto> Clients { get; set; } = new DataCollection<ClientDto>();
    public async Task OnGet()
    {
        Products = (await _productProxy.GetAllAsync(1, 100))!;
        Clients = (await _clientProxy.GetAllAsync(1, 100))!;
    }
    public async Task<IActionResult> OnPostAsync([FromBody] OrderCreateCommand productDto)
    {
        await _orderProxy.CreateAsync(productDto);
        return this.StatusCode(200);
    }
}
