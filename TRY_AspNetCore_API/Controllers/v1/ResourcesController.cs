using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TRY_AspNetCore_API.ActionFilters;

namespace TRY_AspNetCore_API.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ResourcesController : ControllerBase
    {
        private readonly ILogger<ResourcesController> _logger;
        private readonly IMapper _mapper;

        public ResourcesController(ILogger<ResourcesController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
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

            // var resourceDto = _mapper.Map<ResourceDtoV1>(resourceDomainModel);

            return Ok("v1 Placeholder");
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateResourceV1()
        {
            return Ok("v1 Placeholder");
        }
    }
}
