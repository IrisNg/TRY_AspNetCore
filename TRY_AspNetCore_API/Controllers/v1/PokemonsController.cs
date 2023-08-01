using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Net;
using System.Text.Json;
using TRY_AspNetCore_API.ActionFilters;
using TRY_AspNetCore_API.Logging;
using TRY_AspNetCore_API.Models.Domain;
using TRY_AspNetCore_API.Models.DTOs.v1;
using TRY_AspNetCore_API.Models.QueryParams.Filters.v1;
using TRY_AspNetCore_API.Models.Responses;
using TRY_AspNetCore_API.Repositories;

namespace TRY_AspNetCore_API.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class PokemonsController : ControllerBase
    {
        private readonly IPokemonRepository _repository;
        private readonly ILogger<PokemonsController> _logger;
        private readonly IMapper _mapper;
        private readonly ILogging _logFormatter;

        public PokemonsController(
            IPokemonRepository repository,
            ILogger<PokemonsController> logger,
            IMapper mapper,
            ILogging logFormatter
            )
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _logFormatter = logFormatter;
        }


        [HttpGet]
        [Route("LoggerDemo")]
        public async Task<IActionResult> GetLoggerDemoV1()
        {
            _logger.LogInformation("Information");
            _logger.LogDebug("Debug");
            _logger.LogWarning("Warning");
            var errorId = Guid.NewGuid();
            _logger.LogError(new Exception("Mimick exception"), _logFormatter.GetLog("readable error message", logId: errorId));

            _logger.LogInformation(
                $"GetLoggerDemoV1 request completed with data: {JsonSerializer.Serialize(new { Desc = "object data" })}"
            );

            return Ok("See logs in console.");
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GetAllPaginated<List<PokemonDtoV1>>>> GetAllPokemonsV1(
            [FromQuery] FilterPokemonV1 filters,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 20)
        {

            _logger.LogWarning(JsonSerializer.Serialize(filters));
            var filtersList = new List<Expression<Func<Pokemon, bool>>>();


            if (filters.Search != null)
            {
                // TODO: change to case insensitive contains
                filtersList.Add(x => x.Name.IndexOf(filters.Search, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            if (filters.StartCreatedDate != null)
            {
                filtersList.Add(x => x.CreatedDate >= filters.StartCreatedDate);
            }

            if (filters.EndCreatedDate != null)
            {
                filtersList.Add(x => x.CreatedDate <= filters.EndCreatedDate);
            }

            Expression<Func<Pokemon, object>> includeTypes = x => x.Types;
            Expression<Func<Pokemon, object>> includeMoves = x => x.Moves;

            // Sort leh?

            var listWithCount = await _repository.GetAllAsync(
                filters: filtersList.Any() ? filtersList.ToArray() : null,
                includes: new[] { includeTypes, includeMoves },
                pageNumber: pageNumber,
                pageSize: pageSize);

            var pokemonsDto = _mapper.Map<List<PokemonDtoV1>>(listWithCount.Data);


            return Ok(
                new SuccessResponse
                {
                    StatusCode = HttpStatusCode.OK,
                    Payload = new GetAllPaginated<List<PokemonDtoV1>>
                    {
                        Items = pokemonsDto,
                        TotalItemsCount = listWithCount.Count,
                        PageNumber = pageNumber,
                    }
                }
            );
        }


        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PokemonDtoV1>> GetByIdPokemonV1([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest(new ErrorResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Please provide a valid id."

                });
            }

            var pokemonDomainModel = await _repository.GetOneByIdAsync(id);

            if (pokemonDomainModel == null)
            {
                return NotFound(new ErrorResponse
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Message = "No resource was found based on the id provided."
                });
            }

            var responseDto = _mapper.Map<PokemonDtoV1>(pokemonDomainModel);

            return Ok(new SuccessResponse
            {
                StatusCode = HttpStatusCode.OK,
                Payload = responseDto
            }); ;
        }



        [HttpPost]
        [ValidateModel]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PokemonDtoV1>> CreatePokemonV1([FromBody] PokemonCreateRequestDtoV1 requestDto)
        {
            if (requestDto == null)
            {
                ModelState.AddModelError("Request Body Error", "Request body is missing, please provide valid request body.");


                return BadRequest(new ErrorResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "One or more validation errors occurred",
                    Payload = ModelState
                });
            }


            //var pokemonDomainModel = _mapper.Map<Pokemon>(requestDto);
            var pokemonDomainModel = new Pokemon
            {
                Name = requestDto.Name
            };

            pokemonDomainModel = await _repository.CreateAsync(pokemonDomainModel);


            var responseDto = _mapper.Map<PokemonDtoV1>(pokemonDomainModel);

            //// TODO:
            //// Create many-to-many relationships after pokemon.Id is assigned
            //var pokemonTypesDomainModel = _mapper.Map<List<PokemonType>>(requestDto.Types);
            //// use repo for pokemontypes
            //responseDto.Types = _mapper.Map<List<PokemonTypeDtoV1>>(pokemonTypesDomainModel);

            //if (requestDto.Moves != null)
            //{
            //    List<PokemonMove> pokemonMovesDomainModel = _mapper.Map<List<PokemonMove>>(requestDto.Moves);

            //    // use repo for pokemonmoves

            //    responseDto.Moves = _mapper.Map<List<PokemonMoveDtoV1>>(pokemonMovesDomainModel);
            //}


            return CreatedAtRoute(
                nameof(GetByIdPokemonV1),
                new { id = responseDto.Id },
                new SuccessResponse
                {
                    StatusCode = HttpStatusCode.Created,
                    Payload = responseDto
                }
            );
        }


        [HttpPut]
        [Route("{id:int}")]
        [ValidateModel]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PokemonDtoV1>> UpdatePokemonV1([FromRoute] int id, [FromBody] PokemonUpdateRequestDtoV1 requestDto)
        {
            if (requestDto == null)
            {
                return BadRequest(new ErrorResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Please provide valid request body."
                });
            }
            if (id <= 0)
            {
                return BadRequest(new ErrorResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Please provide a valid id."
                });
            }

            if (id != requestDto.Id)
            {
                return BadRequest(new ErrorResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "id from url parameter needs to match Id from request body."
                });
            }

            var pokemonDomainModel = _mapper.Map<Pokemon>(requestDto);

            pokemonDomainModel = await _repository.UpdateByIdAsync(id, pokemonDomainModel);

            // update pokemonType and pokemonMove also

            if (pokemonDomainModel == null)
            {
                return NotFound(new ErrorResponse
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Message = "No resource was found based on the id provided."
                });
            }

            var responseDto = _mapper.Map<PokemonDtoV1>(pokemonDomainModel);

            return Ok(new SuccessResponse
            {
                StatusCode = HttpStatusCode.OK,
                Payload = responseDto
            });
        }


        [HttpDelete]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PokemonDtoV1>> DeletePokemonV1([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest(new ErrorResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Please provide a valid id."
                });
            }

            var pokemonDomainModel = await _repository.DeleteByIdAsync(id);

            if (pokemonDomainModel == null)
            {
                return NotFound(new ErrorResponse
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Message = "No resource was found based on the id provided."
                });
            }

            var responseDto = _mapper.Map<PokemonDtoV1>(pokemonDomainModel);

            return Ok(new SuccessResponse
            {
                StatusCode = HttpStatusCode.OK,
                Payload = responseDto
            });
        }


    }
}
