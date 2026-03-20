using System.Net;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Domain.Exceptions;

namespace TaskManager.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(context, e);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var (statusCode, title) = exception switch
            {
                DomainException => (HttpStatusCode.BadRequest, "Domain Error"),
                NotFoundException => (HttpStatusCode.NotFound, "Resource Not Found"),
                _ => (HttpStatusCode.InternalServerError, "Internal Server Error")
            };

            if (statusCode == HttpStatusCode.InternalServerError)
            {
                _logger.LogError(exception, "Unhandled exception");
            }
            else
            {
                _logger.LogWarning(exception, "Handled domain exception");
            }

            var problem = new ProblemDetails
            {
                Status = (int)statusCode,
                Title = title,
                Detail = exception.Message
            };

            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/problem+json";

            await context.Response.WriteAsJsonAsync(problem);
        }
    }
}
