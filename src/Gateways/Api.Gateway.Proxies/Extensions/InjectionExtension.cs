using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Gateway.Proxies.Extensions;

public static class InjectionExtension
{
    public static IServiceCollection AddServiceProxy(this IServiceCollection services, IConfiguration configuration)
    {
        //services.Configure<ApiUrls>(opt => configuration.GetSection("ApiUrls").Bind(opt));
        services.Configure<ApiUrls>(opt => configuration.GetSection("ApiUrls").Bind(opt));
        services.AddHttpClient<ICatalogHttpProxy, CatalogHttpProxy>();
        services.AddHttpClient<ICustomerProxy, CustomerProxy>();
        services.AddHttpClient<IOrderProxy, OrderProxy>();
        return services;
    }
}
