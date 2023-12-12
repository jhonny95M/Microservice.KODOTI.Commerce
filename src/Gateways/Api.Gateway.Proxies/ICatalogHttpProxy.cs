using Api.Gateway.Models;
using Api.Gateway.Models.Catalog.DTOs;

namespace Api.Gateway.Proxies;

public interface ICatalogHttpProxy
{
    Task<DataCollection<ProductDto>?> GetAllAsync(int page, int take, IEnumerable<int>? clients = null);
    Task<ProductDto?> GetAsync(int id);
}
