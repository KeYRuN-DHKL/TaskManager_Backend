using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;
using TaskManager.Core.TaskManagerExceptions;

namespace TaskManager.API.MiddleWare
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }catch(Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occured: {message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            var (statusCode, Message) = ex switch
            {
                AppException appEx => (appEx.StatusCode,appEx.Message),
                DbUpdateException => (HttpStatusCode.Conflict, "Database Update Error"),
                _ => (HttpStatusCode.InternalServerError, "Internal Server Error")
            };

            context.Response.StatusCode = (int)statusCode;

            var response = new
            {
                success = false,
                statuscode = (int)statusCode,
                message = Message,
                timeStamp = DateTime.UtcNow
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
