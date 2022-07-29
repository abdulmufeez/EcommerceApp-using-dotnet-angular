using AppAPI.Extensions;
using AppAPI.Helpers;
using AppAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ApplicationServices(builder.Configuration);

builder.Services.AddAutoMapper(typeof(MappingProfiles));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.SwaggerdocumentationExtension();





var app = builder.Build();

await app.UseAutoMigratonsAsync();

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
