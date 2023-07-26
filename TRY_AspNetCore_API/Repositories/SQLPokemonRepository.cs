using TRY_AspNetCore_API.Data;
using TRY_AspNetCore_API.Models.Domain;

namespace TRY_AspNetCore_API.Repositories
{
    public class SQLPokemonRepository : SQLRepository<ApplicationDbContext, Pokemon>, IPokemonRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SQLPokemonRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
