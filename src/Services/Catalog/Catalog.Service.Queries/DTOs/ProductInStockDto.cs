namespace Catalog.Service.Queries.DTOs;

public record ProductInStockDto
{
    public int ProductInStockId { get; set; }
    public int ProductId { get; set; }
    public int Stock { get; set; }
}
