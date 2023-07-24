using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace TRY_AspNetCore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourcesController : ControllerBase
    {
        private readonly ILogger<ResourcesController> logger;

        public ResourcesController(ILogger<ResourcesController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllResources()
        {
            logger.LogInformation("Information");
            logger.LogDebug("Debug");
            logger.LogWarning("Warning");
            logger.LogError("Error");

            logger.LogInformation(
                $"GetAllResources request completed with data: {JsonSerializer.Serialize(new { Desc = "object data" })}"
            );
            return Ok("Placeholder");
        }
    }
}
