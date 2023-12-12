using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Customer.Service.EventHandlers.Extensions;

public static class InjectionExtension
{
    public static IServiceCollection AddInjectionServiceEnventHandler(this IServiceCollection service)
    {
        service.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        return service;
    }
}
