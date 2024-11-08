using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;

namespace TaskManager.Web.Middleware
{
    public class GlobalExceptionHandlerMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;
        private readonly IStringLocalizer<GlobalExceptionHandlerMiddleware> _localizer;

        public GlobalExceptionHandlerMiddleware(
            ILogger<GlobalExceptionHandlerMiddleware> logger,
            IStringLocalizer<GlobalExceptionHandlerMiddleware> localizer)
        {
            _logger = logger;
            _localizer = localizer;
        }

        public async System.Threading.Tasks.Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error");

                await HandleExceptionAsync(context, ex);
            }
        }

        private System.Threading.Tasks.Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected

            //if (exception is Exception)
            //{
            //    code = HttpStatusCode.NotFound;
            //}
            // else if (exception is MyUnauthorizedException) code = HttpStatusCode.Unauthorized;
            // else if (exception is MyException)             code = HttpStatusCode.BadRequest;

            var result = JsonConvert.SerializeObject(new
            {
                Success = false,
                message = exception.Message,
                messageDetail = exception.Message
            });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);

        }
    }
}
