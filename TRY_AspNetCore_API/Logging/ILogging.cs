namespace TRY_AspNetCore_API.Logging
{
    public interface ILogging
    {
        public string GetLog(string message, string? type = "ERROR", Guid? logId = null);
    }
}
