using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Nanoblog.Api.Data.Exception;
using Newtonsoft.Json;

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
			var response = new { message = exception.Message };
			var payload = JsonConvert.SerializeObject(response);
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = 400;

			return context.Response.WriteAsync(payload);
		}
    }
}
