using API.Common.Extensions;  // For ExceptionHelper
using API.Constants;          // For EventCodes, Messages, etc.

namespace API.Middleware
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
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var details = ExceptionHelper.GetDetailedError(ex);
            var correlationId = Guid.NewGuid().ToString();  // Optional: generate or pass if you already use

            // Example: log to file, console, or your DB log helper
            _logger.LogError(ex, "Unhandled exception: {Details} | CorrelationId: {CorrelationId}", details, correlationId);

            // Optional: Use your LogHelper if needed
            // await LogHelper.LogErrorAsync(sqlLogger, EventCodes.General.InternalError, correlationId, "Unhandled exception", details);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var response = new {
                message = "An unexpected error occurred.",
                technicalDetails = details,
                correlationId
            };

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
