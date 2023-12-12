using Identity.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Persistence.DataBase.Extensions
{
    public static class InjectionExtension
    {
        public static IServiceCollection AddPersistenceDataBase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"),c=>c.MigrationsHistoryTable("__EFMigrationsHistory","Identity")));
            
            return services;
        }
    }
}
