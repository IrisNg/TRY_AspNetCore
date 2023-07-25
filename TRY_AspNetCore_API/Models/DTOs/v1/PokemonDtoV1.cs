using TRY_AspNetCore_API.Models.Domain;
using Type = TRY_AspNetCore_API.Models.Domain.Type;

namespace TRY_AspNetCore_API.Models.DTOs.v1
{
    public class PokemonDtoV1
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<PokemonTypeDtoV1> Types { get; } = new();

        public List<MoveDtoV1> Moves { get; } = new();

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
