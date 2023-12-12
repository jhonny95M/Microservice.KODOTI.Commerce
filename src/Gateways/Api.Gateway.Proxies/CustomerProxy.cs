using Api.Gateway.Models;
using Api.Gateway.Models.Customer.DTOs;
using Api.Gateway.Proxies.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Api.Gateway.Proxies;

public interface ICustomerProxy
{
    Task<DataCollection<ClientDto>?> GetAllAsync(int page,int take,IEnumerable<int>? clients=null);
    Task<ClientDto?> GetByIdAsync(int id);
}
public class CustomerProxy : ICustomerProxy
{
    private readonly ApiUrls _apiUrls;
    private readonly HttpClient _httpClient;

    public CustomerProxy(
        HttpClient httpClient,
        IOptions<ApiUrls> apiUrls,
        IHttpContextAccessor httpContextAccessor)
    {
        httpClient.AddBearerToken(httpContextAccessor);

        _httpClient = httpClient;
        _apiUrls = apiUrls.Value;
    }
    public async Task<DataCollection<ClientDto>?> GetAllAsync(int page, int take, IEnumerable<int>? clients = null)
    {
        var ids = string.Join(',', clients ?? new List<int>());

        var request = await _httpClient.GetAsync($"{_apiUrls.CustomerUrl}api/client?page={page}&take={take}&ids={ids}");
        request.EnsureSuccessStatusCode();

        return JsonSerializer.Deserialize<DataCollection<ClientDto>>(
            await request.Content.ReadAsStringAsync(),
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }
        );
    }

    public async Task<ClientDto?> GetByIdAsync(int id)
    {
        var request = await _httpClient.GetAsync($"{_apiUrls.CustomerUrl}api/client/{id}");
        request.EnsureSuccessStatusCode();

        return JsonSerializer.Deserialize<ClientDto>(
            await request.Content.ReadAsStringAsync(),
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }
        );
    }
}
