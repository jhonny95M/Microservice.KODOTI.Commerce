using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Service.Queries.Extensions;

public static class InjectionExtension
{
    public static IServiceCollection AddInjectionServiceQuery(this IServiceCollection services)
    {
        services.AddTransient<IProductQueryService, ProductQueryService>();
        return services;
    }
}
