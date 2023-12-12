using Microsoft.Extensions.DependencyInjection;

namespace Customer.Service.Queries.Extensions
{
    public static class InjectionExtension
    {
        public static IServiceCollection AddInjectionServiceQuery(this IServiceCollection services)
        {
            services.AddTransient<IClientQueryService, ClientQueryService>();
            return services;
        }
    }
}
