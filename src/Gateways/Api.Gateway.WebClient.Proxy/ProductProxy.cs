﻿using Api.Gateway.Models;
using Api.Gateway.Models.Catalog.DTOs;
using Api.Gateway.WebClient.Proxy.Configuration;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Api.Gateway.WebClient.Proxy;
public interface IProductProxy
{
    Task<DataCollection<ProductDto>?> GetAllAsync(int page, int take);
}
public class ProductProxy : IProductProxy
{
    private readonly string _apiGatewayUrl;
    private readonly HttpClient _httpClient;

    public ProductProxy(
        HttpClient httpClient,
        ApiGatewayUrl apiGatewayUrl,
        IHttpContextAccessor httpContextAccessor)
    {
        httpClient.AddBearerToken(httpContextAccessor);
        _httpClient = httpClient;
        _apiGatewayUrl = apiGatewayUrl.Value;
    }

    public async Task<DataCollection<ProductDto>?> GetAllAsync(int page, int take)
    {
        var request = await _httpClient.GetAsync($"{_apiGatewayUrl}api/product?page={page}&take={take}");
        request.EnsureSuccessStatusCode();

        return JsonSerializer.Deserialize<DataCollection<ProductDto>>(
            await request.Content.ReadAsStringAsync(),
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }
        );
    }
}