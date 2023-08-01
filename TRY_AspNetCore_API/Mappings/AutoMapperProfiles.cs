using AutoMapper;
using TRY_AspNetCore_API.Models.Domain;
using TRY_AspNetCore_API.Models.DTOs.v1;
using Type = TRY_AspNetCore_API.Models.Domain.Type;

namespace TRY_AspNetCore_API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            /* v1 Resource */

            CreateMap<Pokemon, PokemonDtoV1>().ReverseMap();
            CreateMap<PokemonCreateRequestDtoV1, Pokemon>();
            CreateMap<PokemonUpdateRequestDtoV1, Pokemon>();
            CreateMap<Type, TypeDtoV1>().ReverseMap();
            CreateMap<Move, MoveDtoV1>().ReverseMap();
            CreateMap<PokemonType, PokemonTypeDtoV1>().ReverseMap();
            CreateMap<PokemonMove, PokemonMoveDtoV1>().ReverseMap();

            /* v2 Resource */
        }
    }
}
