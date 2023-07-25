using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TRY_AspNetCore_API.Models.Domain
{
    public class PokemonMove
    {
        [ForeignKey("Pokemon")]
        public int PokemonId { get; set; }
        [ForeignKey("Move")]
        public int MoveId { get; set; }

        public Pokemon Pokemon { get; set; } = null!;
        public Move Move { get; set; } = null!;

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public PokemonMove()
        {
            this.CreatedDate = DateTime.UtcNow;
            this.UpdatedDate = DateTime.UtcNow;
        }
    }
}
