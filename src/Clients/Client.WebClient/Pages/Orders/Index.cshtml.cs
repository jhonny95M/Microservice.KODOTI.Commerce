using Api.Gateway.Models;
using Api.Gateway.Models.Order.DTOs;
using Api.Gateway.WebClient.Proxy;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Client.WebClient.Pages.Orders;
[Authorize(AuthenticationSchemes =CookieAuthenticationDefaults.AuthenticationScheme)]
public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> logger;
    private readonly IOrderProxy orderProxy;

    public IndexModel(ILogger<IndexModel> logger, IOrderProxy orderProxy)
    {
        this.logger = logger;
        this.orderProxy = orderProxy;
    }
    public DataCollection<OrderDto>? Orders { get; set; }
    [BindProperty(SupportsGet = true)]
    public int CurrentPage { get; set; } = 1;
    public async Task OnGet()
    {
        Orders = await orderProxy.GetAllAsync(CurrentPage, 10);
    }
}
