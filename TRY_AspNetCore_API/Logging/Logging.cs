namespace TRY_AspNetCore_API.Logging
{
    public class Logging : ILogging
    {
        public string GetLog(string message, string? type = "ERROR", Guid? logId = null)
        {
            var formattedLogId = logId != null ? $"({logId})" : "";
            return $"{type}{formattedLogId}: {message}";
        }
    }
}
