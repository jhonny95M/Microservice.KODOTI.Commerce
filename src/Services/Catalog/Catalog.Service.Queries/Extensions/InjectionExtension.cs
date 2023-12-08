using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Service.Queries.Extensions
{
    public static class InjectionExtension
    {
        public static IServiceCollection AddInjectionServiceQuery(this IServiceCollection services)
        {
            services.AddTransient<IProductQueryService, ProductQueryService>();
            return services;
        }
    }
}
