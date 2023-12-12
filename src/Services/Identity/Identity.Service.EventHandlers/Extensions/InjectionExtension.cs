using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Identity.Service.EventHandlers.Extensions
{
    public static class InjectionExtension
    {
        public static IServiceCollection AddServiceEventHandler(this IServiceCollection services)
        {
            services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            return services;
        }
    }
}
