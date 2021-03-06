using Microsoft.AspNetCore.Http;
using Nanoblog.Common.Dto;
using Nanoblog.Common.Exception;
using System.Threading.Tasks;
using System.Text.Json;

namespace Nanoblog.Api.Middleware
{
    public class ErrorHandler
    {
        readonly RequestDelegate _next;

        public ErrorHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ApiException ex)
            {
                await HandleApiErrorAsync(context, ex);
            }
        }

        private static Task HandleApiErrorAsync(HttpContext context, ApiException exception)
        {
            var response = new ErrorDto(exception.Message);

            var payload = JsonSerializer.Serialize(response, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 400;

            return context.Response.WriteAsync(payload);
        }
    }
}
