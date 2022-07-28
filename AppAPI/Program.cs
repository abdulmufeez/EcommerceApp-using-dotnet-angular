
using AppAPI.Errors;
using AppAPI.Extensions;
using AppAPI.Middlewares;
using AppAPI_Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ApplicationServices(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

// telling app to give which type of response when strict with error array  
builder.Services.Configure<ApiBehaviorOptions>(options => 
{
    options.InvalidModelStateResponseFactory = actionContext => 
    {
        // extracting errors from modelcontext(httpcontext)
        var errors = actionContext.ModelState
            .Where(e => e.Value.Errors.Count > 0 )
            .SelectMany(x => x.Value.Errors)
            .Select(x => x.ErrorMessage).ToArray();

        var errorResponse = new ApiValidationErrorResponse
        {
            Errors = errors
        };

        return new BadRequestObjectResult(errorResponse);
    };
});

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

app.MapControllers();

app.Run();
