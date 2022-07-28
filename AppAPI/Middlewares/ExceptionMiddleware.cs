using System.Net;
using System.Text.Json;
using AppAPI.Errors;

namespace AppAPI.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;
        public ExceptionMiddleware(RequestDelegate next, 
            ILogger<ExceptionMiddleware> logger, 
            IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        // middleware method 
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

                var response = _env.IsDevelopment()
                    ? new ApiException((int) HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace?.ToString())
                    : new ApiException((int) HttpStatusCode.InternalServerError, "Internal Server Error",null);

                //sending back response in json format
                var options = new JsonSerializerOptions{PropertyNamingPolicy = JsonNamingPolicy.CamelCase};
                var jsonResponse = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(jsonResponse);
            }
        }
    }
}