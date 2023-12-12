using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Catalog.Service.EventHandlers.Extensions;

public static class InjectionExtension
{
    public static IServiceCollection AddInjectionServiceEventHandler(this IServiceCollection services)
    {
        services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        return services;
    }
}
