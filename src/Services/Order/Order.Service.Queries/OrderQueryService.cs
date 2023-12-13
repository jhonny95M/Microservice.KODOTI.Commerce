using Microsoft.EntityFrameworkCore;
using Order.Domain;
using Order.Persistence.DataBase;
using Order.Service.Queries.DTOs;
using Service.Common.Collection;
using Service.Common.Mapping;
using Service.Common.Paging;

namespace Order.Service.Queries;

public interface IOrderQueryService
{
    Task<DataCollection<OrderDto>> GetAllAsync(int page, int take);
    Task<OrderDto> GetByIdAsync(int id);
}
public class OrderQueryService : IOrderQueryService
{
    private readonly ApplicationDbContext context;

    public OrderQueryService(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<DataCollection<OrderDto>> GetAllAsync(int page, int take)
    {
        var collection= await context.Orders
                .Select(o =>
                new Domain.Order
                {
                    ClientId = o.ClientId,
                    OrderId = o.OrderId,
                    CreatedAt = o.CreatedAt,
                    PaymentType = o.PaymentType,
                    Status = o.Status,
                    Total = o.Total,
                    Items = o.Items.Select(c => new OrderDetail
                    {
                        OrderDetailId = c.OrderDetailId,
                        OrderId = c.OrderId,
                        ProductId = c.ProductId,
                        Quantity = c.Quantity,
                        Total = c.Total,
                        UnitPrice = c.UnitPrice,
                    }).ToList(),
                })
            .OrderByDescending(c=>c.OrderId)
            .GetPagedAsync(page, take);
        return collection.MapTo<DataCollection<OrderDto>>()!;
    }

    public async Task<OrderDto> GetByIdAsync(int id)
    {
        try
        {
            var order = await context.Orders
                .Where(c => c.OrderId == id)
                .Select(o =>
                new Domain.Order
                {
                    ClientId= o.ClientId,
                    OrderId = o.OrderId,
                    CreatedAt=o.CreatedAt,
                    PaymentType = o.PaymentType,
                    Status = o.Status,
                    Total = o.Total,
                    Items=o.Items.Select(c=>new OrderDetail
                    {
                        OrderDetailId = c.OrderDetailId,
                        OrderId = c.OrderId,
                        ProductId = c.ProductId,
                        Quantity = c.Quantity,
                        Total = c.Total,
                        UnitPrice = c.UnitPrice,
                    }).ToList(),
                })
                .SingleOrDefaultAsync();
                
                //(await context.Orders                               
                //.SingleOrDefaultAsync(c => c.OrderId == id))?
                //.Items.Select(c => new OrderDetail
                //{
                //    OrderDetailId = c.OrderDetailId,
                //    OrderId = c.OrderId,
                //    ProductId = c.ProductId,
                //    Quantity = c.Quantity,
                //    Total = c.Total,
                //    UnitPrice = c.UnitPrice,
                //});
            return order!.MapTo<OrderDto>()!;
        }catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }
    }
}