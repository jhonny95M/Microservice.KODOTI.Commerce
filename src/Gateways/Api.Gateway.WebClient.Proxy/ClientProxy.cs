using Api.Gateway.Models;
using Api.Gateway.Models.Customer.DTOs;
using Api.Gateway.WebClient.Proxy.Configuration;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Text.Json;

namespace Api.Gateway.WebClient.Proxy;

public interface IClientProxy
{
    Task<DataCollection<ClientDto>?> GetAllAsync(int page,int take);
}
public class ClientProxy : IClientProxy
{
    private readonly string apiGatewayUrl;
    private readonly HttpClient httpClient;

    public ClientProxy(ApiGatewayUrl apiGatewayUrl, HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
    {
        httpClient.AddBearerToken(httpContextAccessor);
        this.apiGatewayUrl = apiGatewayUrl.Value;
        this.httpClient = httpClient;
    }

    public async Task<DataCollection<ClientDto>?> GetAllAsync(int page, int take)
    {
        var request = await httpClient.GetAsync($"{apiGatewayUrl}api/client?page={page}&take={take}");
        request.EnsureSuccessStatusCode();

        return JsonSerializer.Deserialize<DataCollection<ClientDto>>(
            await request.Content.ReadAsStringAsync(),
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }
        );
    }
}
