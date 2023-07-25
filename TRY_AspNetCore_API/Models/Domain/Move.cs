using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TRY_AspNetCore_API.Models.Domain
{
    public class Move
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string? Url { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Type { get; set; }
        public int? Power { get; set; }
        public int? Accuracy { get; set; }
        public int? PP { get; set; }

        public ICollection<PokemonMove> Pokemons { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public Move()
        {
            this.CreatedDate = DateTime.UtcNow;
            this.UpdatedDate = DateTime.UtcNow;
        }
    }
}
