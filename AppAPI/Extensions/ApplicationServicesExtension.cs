using AppAPI.Errors;
using AppAPI_Core.Interfaces;
using AppAPI_Infrastructure.Data;
using AppAPI_Infrastructure.Respositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppAPI.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection ApplicationServices(this IServiceCollection services,
           IConfiguration config)
        {
            // services dependancy injection
            //================================================================

            // this is used when we dont know which type T is inserted in service
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            // db configs
            services.AddDbContext<StoreDataContext>(x =>
            {
                x.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });

            // telling app to give which type of response when strict with error array  
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    // extracting errors from modelcontext(httpcontext)
                    var errors = actionContext.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .SelectMany(x => x.Value.Errors)
                        .Select(x => x.ErrorMessage).ToArray();

                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });

            return services;
        }
    }
}