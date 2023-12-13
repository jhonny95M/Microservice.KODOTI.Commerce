using Api.Gateway.Models.Order.DTOs;
using Api.Gateway.WebClient.Proxy;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Client.WebClient.Pages.Orders;

[Authorize(AuthenticationSchemes =CookieAuthenticationDefaults.AuthenticationScheme)]
public class DetailModel : PageModel
{
    private readonly ILogger<DetailModel> _logger;
    private readonly IOrderProxy _orderProxy;

    public DetailModel(ILogger<DetailModel> logger, IOrderProxy orderProxy)
    {
        _logger = logger;
        _orderProxy = orderProxy;
    }

    public OrderDto Order { get; set; }
    public async Task OnGet(int id)
    {
        Order = (await _orderProxy.GetAsync(id))!;
    }
}
