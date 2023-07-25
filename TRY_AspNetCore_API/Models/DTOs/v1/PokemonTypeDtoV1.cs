using System.ComponentModel.DataAnnotations.Schema;
using TRY_AspNetCore_API.Models.Domain;

namespace TRY_AspNetCore_API.Models.DTOs.v1
{
    public class PokemonTypeDtoV1
    {
        public int Slot { get; set; }

        public int PokemonId { get; set; }

        public int TypeId { get; set; }

        public TypeDtoV1 Type { get; set; } = null!;
    }
}
