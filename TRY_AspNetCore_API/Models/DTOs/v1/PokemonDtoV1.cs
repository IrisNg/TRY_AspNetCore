using System.ComponentModel.DataAnnotations;
using TRY_AspNetCore_API.Models.Domain;
using Type = TRY_AspNetCore_API.Models.Domain.Type;

namespace TRY_AspNetCore_API.Models.DTOs.v1
{
    public class PokemonDtoV1
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<PokemonTypeDtoV1> Types { get; set; }

        public List<PokemonMoveDtoV1> Moves { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
