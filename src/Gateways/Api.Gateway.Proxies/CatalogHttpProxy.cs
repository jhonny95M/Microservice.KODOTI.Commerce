using Api.Gateway.Models;
using Api.Gateway.Models.Catalog.DTOs;
using Api.Gateway.Proxies.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Api.Gateway.Proxies;


public class CatalogHttpProxy : ICatalogHttpProxy
{
    private readonly ApiUrls apiUrls;
    private readonly HttpClient httpClient;

    public CatalogHttpProxy(IOptions<ApiUrls> options, HttpClient httpClient,IHttpContextAccessor httpContextAccessor)
    {
        httpClient.AddBearerToken(httpContextAccessor);
        apiUrls = options.Value;
        this.httpClient = httpClient;
    }

    public async Task<DataCollection<ProductDto>?> GetAllAsync(int page, int take, IEnumerable<int>? clients = null)
    {
        var ids = string.Join(',', clients ?? new List<int>());

        var request = await httpClient.GetAsync($"{apiUrls.CatalogUrl}api/product?page={page}&take={take}&ids={ids}");
        request.EnsureSuccessStatusCode();

        return JsonSerializer.Deserialize<DataCollection<ProductDto>>(
            await request.Content.ReadAsStringAsync(),
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }
        );
    }

    public async Task<ProductDto?> GetAsync(int id)
    {
        var request = await httpClient.GetAsync($"{apiUrls.CatalogUrl}api/product/{id}");
        request.EnsureSuccessStatusCode();

        return JsonSerializer.Deserialize<ProductDto>(
            await request.Content.ReadAsStringAsync(),
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }
        );
    }
}
