using System.ComponentModel.DataAnnotations;

namespace TRY_AspNetCore_API.Models.DTOs.v1
{
    public class PokemonCreateRequestDtoV1
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Name can only contain up to 50 characters.")]
        public string Name { get; set; }

        [Required]
        public List<PokemonTypeDtoV1> Types { get; set; }

        public List<PokemonMoveDtoV1>? Moves { get; set; }
    }
}
