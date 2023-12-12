using MediatR;
using static Catalog.Common.Enums;

namespace Catalog.Service.EventHandlers.Commands;

public record ProductInStockUpdateStokCommand : INotification
{
    public IEnumerable<ProductInStockUpdateItem> Items { get; set; } = new List<ProductInStockUpdateItem>();
}
public record ProductInStockUpdateItem
{
    public int ProductId { get; set; }
    public int Stock { get; set; }
    public ProductInStockAction Action { get; set; }
}