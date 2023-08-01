using System.Net;

namespace TRY_AspNetCore_API.Models.Responses
{
    public class ErrorResponse
    {
        public bool IsSuccess { get; set; } = false;

        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.InternalServerError;

        public Guid? ErrorId { get; set; }
        public string Message { get; set; } = "Something went wrong on the server side.";

        public object? Payload { get; set; }

    }
}
