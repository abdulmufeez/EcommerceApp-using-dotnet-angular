using Microsoft.OpenApi.Models;

namespace AppAPI.Extensions
{
    public static class SwaggerServiceExtension
    {
        public static IServiceCollection SwaggerdocumentationExtension(this IServiceCollection services)
        {
            services.AddSwaggerGen(configOptions =>
            {
                // creating a doc of apis endpoint with versioning 
                configOptions.SwaggerDoc("v1", new OpenApiInfo { Title = "EcommerceApp", Version = "v1" });
            });

            return services;
        }

        public static WebApplication UseSwaggerDocumentation(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(config =>
            {
                //endpoint url              // swagger document name
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "EccomerceApp API v1");
            });

            return app;
        }
    }
}