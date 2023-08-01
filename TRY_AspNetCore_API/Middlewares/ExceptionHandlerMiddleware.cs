using System.Net;
using TRY_AspNetCore_API.Logging;
using TRY_AspNetCore_API.Models.Responses;

namespace TRY_AspNetCore_API.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        private readonly ILogging _logFormatter;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger, ILogging logFormatter)
        {
            _next = next;
            _logger = logger;
            _logFormatter = logFormatter;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                var statusCode = HttpStatusCode.InternalServerError;

                var errorId = Guid.NewGuid();

                var body = new ErrorResponse
                {
                    StatusCode = statusCode,
                    ErrorId = errorId,
                    Message = "Something went wrong on the server side."
                };

                // Log error
                _logger.LogError(ex, _logFormatter.GetLog(ex.Message, logId: errorId));

                // Response headers and body
                httpContext.Response.StatusCode = (int)statusCode;
                httpContext.Response.ContentType = "application/json";

                await httpContext.Response.WriteAsJsonAsync(body);
            }
        }
    }
}
