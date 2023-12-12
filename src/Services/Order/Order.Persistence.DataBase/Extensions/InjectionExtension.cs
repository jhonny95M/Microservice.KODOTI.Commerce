using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Order.Persistence.DataBase.Extensions
{
    public static class InjectionExtension
    {
        public static IServiceCollection AddInjectionPersistenceDataBase(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("OrderConnection"), c => c.MigrationsHistoryTable("__EFMigrationsHistory", "Order")));
            return services;
        }
    }
}
