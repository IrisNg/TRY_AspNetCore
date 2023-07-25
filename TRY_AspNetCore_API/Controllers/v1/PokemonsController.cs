using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TRY_AspNetCore_API.ActionFilters;
using TRY_AspNetCore_API.Models.DTOs.v1;

namespace TRY_AspNetCore_API.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class PokemonsController : ControllerBase
    {
        private readonly ILogger<PokemonsController> _logger;
        private readonly IMapper _mapper;

        public PokemonsController(ILogger<PokemonsController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }


        [HttpGet]
        [Route("LoggerDemo")]
        public async Task<IActionResult> GetLoggerDemoV1()
        {
            _logger.LogInformation("Information");
            _logger.LogDebug("Debug");
            _logger.LogWarning("Warning");
            _logger.LogError("Error");

            _logger.LogInformation(
                $"GetLoggerDemoV1 request completed with data: {JsonSerializer.Serialize(new { Desc = "object data" })}"
            );

            return Ok("v1 Placeholder");
        }


        [HttpGet]
        public async Task<ActionResult<List<PokemonDtoV1>>> GetAllPokemonsV1()
        {

            return Ok("GetAllPokemonsV1");
        }

        [HttpPost]
        [ValidateModel]
        public async Task<ActionResult<PokemonDtoV1>> CreatePokemonV1()
        {
            var pokemonDto = new PokemonDtoV1();

            return CreatedAtRoute("GetByIdPokemonV1", new { id = pokemonDto.Id }, pokemonDto);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<PokemonDtoV1>> GetByIdPokemonV1([FromRoute] int id)
        {
            var pokemonDto = new PokemonDtoV1();
            return Ok(pokemonDto);
        }
    }
}
