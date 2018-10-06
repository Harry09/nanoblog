using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

using Newtonsoft.Json;

using Nanoblog.Core.Data.Exception;
using Nanoblog.Core.Data.Dto;

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
			var payload = JsonConvert.SerializeObject(response);
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = 400;

			return context.Response.WriteAsync(payload);
		}
    }
}
