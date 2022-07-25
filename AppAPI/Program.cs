
using AppAPI.Extensions;
using AppAPI_Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ApplicationServices(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// enabling auto migrations
using var appScope = app.Services.CreateScope();
var appServices = appScope.ServiceProvider;
try
{
    var dbContext = appServices.GetService<StoreDataContext>();
    await dbContext.Database.MigrateAsync();
}
catch (Exception ex)
{
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, ex.Message);    
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
