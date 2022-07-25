using AppAPI_Core.Interfaces;
using AppAPI_Core.Respositories;
using AppAPI_Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AppAPI.Extensions
{
    public static class ApplicationServicesExtension
    {
         public static IServiceCollection ApplicationServices (this IServiceCollection services,
            IConfiguration config)
        {
            // services dependancy injection
            services.AddScoped<IProductRepository, ProductRespository>();

            // db configs
            services.AddDbContext<StoreDataContext>( x => {
                x.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });

            return services;
        }
    }
}