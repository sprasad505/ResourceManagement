using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Builder;

namespace App.DAL.Middlewares
{
    public class GlobalExceptionHandlingMiddleware : IMiddleware
    {
        //private readonly RequestDelegate _next; //1
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

        public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (APIException ex)
            {
                context.Response.StatusCode = (int)ex.StatusCode;

                ProblemDetails problem = new()
                {
                    Status = ex.StatusCode,
                    Detail = ex.Message
                };

                string json = JsonConvert.SerializeObject(problem);
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(json);

                return;
            }
        }
    }
    public static class ExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalExceptionHandlingMiddleware>();
        }
    }
}