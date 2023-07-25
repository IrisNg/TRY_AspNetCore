using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TRY_AspNetCore_API.Models.Domain
{
    public class Pokemon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public List<TypePokemon> TypePokemons { get; } = new();
        public List<Type> Types { get; } = new();

        public List<MovePokemon> MovePokemons { get; } = new();
        public List<Move> Moves { get; } = new();

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public Pokemon()
        {
            this.CreatedDate = DateTime.UtcNow;
            this.UpdatedDate = DateTime.UtcNow;
        }
    }
}
