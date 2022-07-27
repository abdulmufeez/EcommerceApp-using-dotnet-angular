using AppAPI_Core.Interfaces;
using AppAPI_Infrastructure.Data;
using AppAPI_Infrastructure.Respositories;
using Microsoft.EntityFrameworkCore;

namespace AppAPI.Extensions
{
    public static class ApplicationServicesExtension
    {
         public static IServiceCollection ApplicationServices (this IServiceCollection services,
            IConfiguration config)
        {
            // services dependancy injection
            
            // this is used when we dont know which type T is inserted in service
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            // db configs
            services.AddDbContext<StoreDataContext>( x => {
                x.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });

            return services;
        }
    }
}