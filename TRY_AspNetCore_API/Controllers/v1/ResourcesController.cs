using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace TRY_AspNetCore_API.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ResourcesController : ControllerBase
    {
        private readonly ILogger<ResourcesController> _logger;

        public ResourcesController(ILogger<ResourcesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllResourcesV1()
        {
            _logger.LogInformation("Information");
            _logger.LogDebug("Debug");
            _logger.LogWarning("Warning");
            _logger.LogError("Error");

            _logger.LogInformation(
                $"GetAllResources request completed with data: {JsonSerializer.Serialize(new { Desc = "object data" })}"
            );
            return Ok("v1 Placeholder");
        }
    }
}
