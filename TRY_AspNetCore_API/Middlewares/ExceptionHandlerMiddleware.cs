using System.Net;

namespace TRY_AspNetCore_API.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                var statusCode = (int)HttpStatusCode.InternalServerError;

                var errorId = Guid.NewGuid();

                var body = new
                {
                    StatusCode = statusCode,
                    IsSuccess = false,
                    ErrorId = errorId,
                    Message = "Something went wrong on the server side."
                };

                // Log error
                _logger.LogError(ex, $"ERROR({errorId}): {ex.Message}");

                // Response headers and body
                httpContext.Response.StatusCode = statusCode;
                httpContext.Response.ContentType = "application/json";

                await httpContext.Response.WriteAsJsonAsync(body);
            }
        }
    }
}
