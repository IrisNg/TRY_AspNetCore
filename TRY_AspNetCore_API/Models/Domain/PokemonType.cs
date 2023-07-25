using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TRY_AspNetCore_API.Models.Domain
{
    public class PokemonType
    {
        public int Slot { get; set; }

        [ForeignKey("Pokemon")]
        public int PokemonId { get; set; }
        [ForeignKey("Type")]
        public int TypeId { get; set; }

        public Pokemon Pokemon { get; set; } = null!;
        public Type Type { get; set; } = null!;

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public PokemonType()
        {
            this.CreatedDate = DateTime.UtcNow;
            this.UpdatedDate = DateTime.UtcNow;
        }
    }
}
