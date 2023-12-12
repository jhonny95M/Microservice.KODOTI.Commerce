using Microsoft.Extensions.DependencyInjection;

namespace Order.Service.Queries.Extensions
{
    public static class InjectionExtension
    {
        public static IServiceCollection AddServiceQuery(this IServiceCollection services)
        {
            services.AddTransient<IOrderQueryService, OrderQueryService>();
            return services;
        }
    }
}
