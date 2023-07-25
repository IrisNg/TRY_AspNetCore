using Microsoft.EntityFrameworkCore;
using TRY_AspNetCore_API.Models.Domain;
using Type = TRY_AspNetCore_API.Models.Domain.Type;

namespace TRY_AspNetCore_API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions)
            : base(dbContextOptions)
        {
        }

        public DbSet<Pokemon> Pokemons { get; set; }
        public DbSet<Type> Types { get; set; }
        public DbSet<Move> Moves { get; set; }

        public DbSet<TypePokemon> TypePokemons { get; set; }
        public DbSet<MovePokemon> MovesPokemons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed entities
            // modelBuilder.Entity<Resource>().HasData(seedData);
        }

    }
}
