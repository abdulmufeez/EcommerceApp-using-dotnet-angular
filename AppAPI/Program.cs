
using AppAPI.Extensions;
using AppAPI_Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ApplicationServices(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
