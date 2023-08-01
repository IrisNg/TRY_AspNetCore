using System.Net;

namespace TRY_AspNetCore_API.Models.Responses
{
    public class SuccessResponse
    {
        public bool IsSuccess { get; set; } = true;

        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;

        public string? Message { get; set; }

        public object? Payload { get; set; }
    }
}
