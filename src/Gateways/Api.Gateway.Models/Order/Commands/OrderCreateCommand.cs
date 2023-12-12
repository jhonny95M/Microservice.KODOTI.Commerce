using static Api.Gateway.Models.Order.Commons.Enums;

namespace Api.Gateway.Models.Order.Commands;

public record OrderCreateCommand 
{
    public OrderStatus Status { get; set; }
    public OrderPayment PaymentType { get; set; }
    public int ClientId { get; set; }
    public ICollection<OrderCreateDetail> Items { get; set; } = new List<OrderCreateDetail>();
    //public DateTime CreatedAt { get; set; }
    //public decimal Total { get; set; }
}
public record OrderCreateDetail
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
