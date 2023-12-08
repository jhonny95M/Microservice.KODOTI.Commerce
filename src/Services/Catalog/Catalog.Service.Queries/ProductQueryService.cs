using Catalog.Persistence.DataBase;
using Catalog.Service.Queries.DTOs;
using Microsoft.EntityFrameworkCore;
using Service.Common.Collection;
using Service.Common.Mapping;
using Service.Common.Paging;

namespace Catalog.Service.Queries;
public interface IProductQueryService
{
    Task<DataCollection<ProductDto>> GetAllAsync(int page, int take, IEnumerable<int>? products = null);
    Task<ProductDto> GetByIdAsync(int id);
}
public class ProductQueryService: IProductQueryService
{
    private readonly ApplicationDbContext context;

    public ProductQueryService(ApplicationDbContext context)
    {
        this.context = context;
    }
    public async Task<DataCollection<ProductDto>> GetAllAsync(int page,int take,IEnumerable<int>? products=null)
    {
        var collection=await context.Products
                        .Where(c=>products==null || products.Contains(c.ProductId))
                        .OrderByDescending(c=>c.ProductId)
                        .GetPagedAsync(page,take);
        return collection.MapTo<DataCollection<ProductDto>>()!;
    }
    public async Task<ProductDto>GetByIdAsync(int id)
    {
        return (await context.Products.SingleOrDefaultAsync(c => c.ProductId == id))!.MapTo<ProductDto>()!;
    }

}
