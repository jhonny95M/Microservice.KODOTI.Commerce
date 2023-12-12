using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Service.Proxies.Catalog;

namespace Order.Service.Proxies.Extensions;

public static class InjectionExtension
{
    public static IServiceCollection  AddServiceProxy(this IServiceCollection services,IConfiguration configuration)
    {
        //services.Configure<ApiUrls>(opt => configuration.GetSection("ApiUrls").Bind(opt));
        services.Configure<AzureServiceBus>(opt => configuration.GetSection("AzureServiceBus").Bind(opt));
        //services.AddHttpClient<ICatalogProxy, CatalogHttpProxy>();
        services.AddTransient<ICatalogProxy, CatalogQueueProxy>();
        return services;
    }
}
