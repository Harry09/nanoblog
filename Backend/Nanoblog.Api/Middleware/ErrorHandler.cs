using Microsoft.AspNetCore.Http;
using Nanoblog.Common.Dto;
using Nanoblog.Common.Exception;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Threading.Tasks;

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

            var serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            var payload = JsonConvert.SerializeObject(response, serializerSettings);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 400;

            return context.Response.WriteAsync(payload);
        }
    }
}
