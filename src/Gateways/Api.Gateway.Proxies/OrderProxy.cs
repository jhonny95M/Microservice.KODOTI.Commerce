using Api.Gateway.Models;
using Api.Gateway.Models.Order.Commands;
using Api.Gateway.Models.Order.DTOs;
using Api.Gateway.Proxies.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace Api.Gateway.Proxies;

public interface IOrderProxy
{
    Task<DataCollection<OrderDto>?> GetAllAsync(int page, int take);
    Task<OrderDto?> GetByIdAsync(int id);
    Task CreateAsync(OrderCreateCommand command);
}
public class OrderProxy:IOrderProxy
{
    private readonly ApiUrls _apiUrls;
    private readonly HttpClient _httpClient;

    public OrderProxy(
        HttpClient httpClient,
        IOptions<ApiUrls> apiUrls,
        IHttpContextAccessor httpContextAccessor)
    {
        httpClient.AddBearerToken(httpContextAccessor);

        _httpClient = httpClient;
        _apiUrls = apiUrls.Value;
    }

    public async Task CreateAsync(OrderCreateCommand command)
    {
        var content = new StringContent(
                JsonSerializer.Serialize(command),
                Encoding.UTF8,
                "application/json"
            );

        var request = await _httpClient.PostAsync($"{_apiUrls.OrderUrl}api/order", content);
        request.EnsureSuccessStatusCode();
    }

    public async Task<DataCollection<OrderDto>?> GetAllAsync(int page, int take)
    {
        var request = await _httpClient.GetAsync($"{_apiUrls.OrderUrl}api/order?page={page}&take={take}");
        request.EnsureSuccessStatusCode();

        return JsonSerializer.Deserialize<DataCollection<OrderDto>>(
            await request.Content.ReadAsStringAsync(),
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }
        );
    }

    public async Task<OrderDto?> GetByIdAsync(int id)
    {
        var request = await _httpClient.GetAsync($"{_apiUrls.OrderUrl}api/order/{id}");
        request.EnsureSuccessStatusCode();

        return JsonSerializer.Deserialize<OrderDto>(
            await request.Content.ReadAsStringAsync(),
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }
        );
    }
}
