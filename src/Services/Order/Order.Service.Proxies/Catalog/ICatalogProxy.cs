using Order.Service.Proxies.Catalog.Commands;

namespace Order.Service.Proxies.Catalog;

public interface ICatalogProxy
{
    Task UpdateStockAsync(ProductInStockUpdateCommand request);
}
