using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Customer.Persistence.DataBase.Extensions
{
    public static class InjectionExtensions
    {
        public static IServiceCollection AddInjectionPersinstenceDataBase(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("CustomerConnection"),x=>x.MigrationsHistoryTable("__EFMigrationsHistory","Customer")));
            return services;
        }
    }
}
