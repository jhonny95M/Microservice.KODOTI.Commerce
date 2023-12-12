using Microsoft.EntityFrameworkCore;
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
        var collection=await context.Orders
            .Include(c=>c.Items)
            .OrderByDescending(c=>c.OrderId)
            .GetPagedAsync(page, take);
        return collection.MapTo<DataCollection<OrderDto>>()!;
    }

    public async Task<OrderDto> GetByIdAsync(int id) =>
        (await context.Orders.Include(c => c.Items).SingleOrDefaultAsync(c => c.OrderId == id))!.MapTo<OrderDto>()!;
}