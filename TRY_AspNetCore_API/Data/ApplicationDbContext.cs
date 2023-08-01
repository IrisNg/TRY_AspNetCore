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

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Many-To-Many

            modelBuilder.Entity<PokemonType>()
                .HasKey(pt => new { pt.PokemonId, pt.TypeId });

            modelBuilder.Entity<PokemonType>()
                .HasOne(pt => pt.Pokemon)
                .WithMany(pt => pt.Types)
                .HasForeignKey(pt => pt.PokemonId);
            modelBuilder.Entity<PokemonType>()
                .HasOne(pt => pt.Type)
                .WithMany(pt => pt.Pokemons)
                .HasForeignKey(pt => pt.TypeId);


            modelBuilder.Entity<PokemonMove>()
                .HasKey(pt => new { pt.PokemonId, pt.MoveId });

            modelBuilder.Entity<PokemonMove>()
                .HasOne(pt => pt.Pokemon)
                .WithMany(pt => pt.Moves)
                .HasForeignKey(pt => pt.PokemonId);
            modelBuilder.Entity<PokemonMove>()
                .HasOne(pt => pt.Move)
                .WithMany(pt => pt.Pokemons)
                .HasForeignKey(pt => pt.MoveId);


            // Seed entities
            // modelBuilder.Entity<Resource>().HasData(seedData);
        }
    }
}
