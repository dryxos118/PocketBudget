using PocketApi.Models;
using System.Text.Json;

namespace PocketApi.Config
{
    public class PocketExceptionMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (PocketActionResult ex)
            {
                await HandleExceptionAsync(context, ex);
            }
            catch (Exception ex2)
            {
                await HandleExceptionAsync(context, new PocketActionResult(ex2.Message, ErrorType.InternalServerError, "Middleware"));
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, PocketActionResult exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 500;
            var result = JsonSerializer.Serialize(new { Error = exception.Message, Type = Enum.GetName(exception.Type), exception.Location });
            return context.Response.WriteAsync(result);
        }
    }
}
