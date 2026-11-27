using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Moka.middleware
{
    public class ExceptionMiddleware
    {
        private RequestDelegate _next;
        private ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        private Task HandleValidationExceptionAsync(HttpContext context, ValidationException ex)
        {
            _logger.LogError(ex, "A data validation exception ocurred.");

            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/json";

            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Data validation error.",
                Detail = ex.Message
            };

            return context.Response.WriteAsJsonAsync(problemDetails);
        }
        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            _logger.LogError(ex, "An unexpected exception ocurred.");

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "An unexpected exception ocurred.",
                Detail = "Please contact the administrator."
            };

            return context.Response.WriteAsJsonAsync(problemDetails);
        }


        public async Task InvokeAsync(HttpContext context) 
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {

                await HandleValidationExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {

                await HandleExceptionAsync(context, ex);
            }
        }
    }
}
