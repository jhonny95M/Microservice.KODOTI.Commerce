namespace Order.Service.Proxies.Catalog.Commands;
public enum ProductInStockAction
{
    Add,
    Substract
}
public record ProductInStockUpdateCommand
{
    public string? AccesToken { get; set; }
    public IEnumerable<ProductInStockUpdateItem> Items { get; set; } = new List<ProductInStockUpdateItem>();
}
public record ProductInStockUpdateItem
{
    public int ProductId { get; set; }
    public int Stock { get; set; }
    public ProductInStockAction Action { get; set; }
}
