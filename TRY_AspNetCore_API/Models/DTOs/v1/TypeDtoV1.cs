using TRY_AspNetCore_API.Models.Domain;

namespace TRY_AspNetCore_API.Models.DTOs.v1
{
    public class TypeDtoV1
    {
        public int Id { get; set; }

        public string? Url { get; set; }
        public string Name { get; set; }

        public List<PokemonDtoV1> Pokemons { get; set; }
    }
}
