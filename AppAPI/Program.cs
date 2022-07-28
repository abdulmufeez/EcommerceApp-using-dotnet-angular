using AppAPI.Extensions;
using AppAPI.Helpers;
using AppAPI.Middlewares;
using AppAPI_Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ApplicationServices(builder.Configuration);

builder.Services.AddAutoMapper(typeof(MappingProfiles));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.SwaggerdocumentationExtension();





var app = builder.Build();

// enabling auto migrations
using (var appScope = app.Services.CreateScope())
{
    var appServices = appScope.ServiceProvider;
    var loggerFactory = appServices.GetRequiredService<ILoggerFactory>();
    try
    {
        var dbContext = appServices.GetRequiredService<StoreDataContext>();
        await dbContext.Database.MigrateAsync();

        await StoreDataContextDataSeed.SeedAsync(dbContext, loggerFactory);
    }
    catch (Exception ex)
    {
        //var logger = app.Services.GetRequiredService<ILogger<Program>>();
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "An Error occurred during migrations... \n" + ex.Message);
    }
}

// adding custome middleware
app.UseMiddleware<ExceptionMiddleware>();

// adding custome error responses
app.UseStatusCodePagesWithReExecute("/error/{0}");

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.UseSwaggerDocumentation();

app.MapControllers();

app.Run();
