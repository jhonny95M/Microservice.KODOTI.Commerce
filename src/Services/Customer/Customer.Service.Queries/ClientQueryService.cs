using Customer.Persistence.DataBase;
using Customer.Service.Queries.DTOs;
using Microsoft.EntityFrameworkCore;
using Service.Common.Collection;
using Service.Common.Mapping;
using Service.Common.Paging;

namespace Customer.Service.Queries;
public interface IClientQueryService
{
    Task<DataCollection<ClientDto>> GetAllAsync(int page = 1, int take = 10, IEnumerable<int>? clientIds = null);
    Task<ClientDto> GetByIdAsync(int id);
}
public class ClientQueryService : IClientQueryService
{
    private readonly ApplicationDbContext context;

    public ClientQueryService(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<DataCollection<ClientDto>> GetAllAsync(int page = 1, int take = 10, IEnumerable<int>? clientIds = null)
    {
        var collection = await context.Clients
                        .Where(c => clientIds == null || clientIds.Contains(c.ClientId))
                        .OrderByDescending(c => c.ClientId)
                        .GetPagedAsync(page, take);
        return collection.MapTo<DataCollection<ClientDto>>()!;
    }

    public async Task<ClientDto> GetByIdAsync(int id)=>
        (await context.Clients.SingleOrDefaultAsync(c => c.ClientId == id))!.MapTo<ClientDto>()!;
}
