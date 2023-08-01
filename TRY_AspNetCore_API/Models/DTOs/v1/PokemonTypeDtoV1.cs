using System.ComponentModel.DataAnnotations;

namespace TRY_AspNetCore_API.Models.DTOs.v1
{
    public class PokemonTypeDtoV1
    {
        [Required]
        public int Slot { get; set; }

        [Required]
        public int PokemonId { get; set; }

        [Required]
        public int TypeId { get; set; }

        public TypeDtoV1? Type { get; set; }
    }
}
