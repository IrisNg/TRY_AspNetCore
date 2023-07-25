using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TRY_AspNetCore_API.Models.Domain
{
    public class Type
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string? Url { get; set; }
        public string Name { get; set; } = string.Empty;

        public List<TypePokemon> TypePokemons { get; } = new();
        public List<Pokemon> Pokemons { get; } = new();

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public Type()
        {
            this.CreatedDate = DateTime.UtcNow;
            this.UpdatedDate = DateTime.UtcNow;
        }
    }
}
