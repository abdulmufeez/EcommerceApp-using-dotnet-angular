using AppAPI_Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AppAPI.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection ApplicationServices (this IServiceCollection services,
            IConfiguration config)
        {
            // db configs
            services.AddDbContext<StoreDataContext>( x => {
                x.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });

            return services;
        }
    }
}