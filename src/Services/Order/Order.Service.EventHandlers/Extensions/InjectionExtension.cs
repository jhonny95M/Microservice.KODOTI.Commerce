using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Order.Service.EventHandlers.Extensions
{
    public static class InjectionExtension
    {
        public static IServiceCollection AddServiceEventHandler(this IServiceCollection services)
        {
            services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            return services;
        }
    }
}
