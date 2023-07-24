using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace TRY_AspNetCore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourcesController : ControllerBase
    {
        private readonly ILogger<ResourcesController> _logger;

        public ResourcesController(ILogger<ResourcesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllResources()
        {
            _logger.LogInformation("Information");
            _logger.LogDebug("Debug");
            _logger.LogWarning("Warning");
            _logger.LogError("Error");

            _logger.LogInformation(
                $"GetAllResources request completed with data: {JsonSerializer.Serialize(new { Desc = "object data" })}"
            );
            return Ok("Placeholder");
        }
    }
}
