using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq.Expressions;
using TRY_AspNetCore_API.Models.Domain;

namespace TRY_AspNetCore_API.Repositories
{
    // Create another custom interface if using composite or GUID-type primary key
    // This generic interface is only for int-type primary key
    public interface IRepository<TEntity>
        where TEntity : class, IEntityHasId
    {
        public Task<WithCount<List<TEntity>>> GetAllAsync(
            Expression<Func<TEntity, bool>>[]? filters = null,
            Expression<Func<TEntity, object>>[]? includes = null,
            Expression<Func<TEntity, object>>[]? sortBy = null,
            bool[]? sortByIsAscending = null,
            int pageNumber = 1,
            int pageSize = 20);

        public Task<TEntity?> GetOneByIdAsync(
            int id,
            Expression<Func<TEntity, object>>[]? includes = null);

        public Task<TEntity?> GetOneByFilterAsync(
            Expression<Func<TEntity, bool>>[] filters,
            Expression<Func<TEntity, object>>[]? includes = null);

        public Task<TEntity> CreateAsync(TEntity entity);

        public Task<TEntity?> UpdateByIdAsync(int id, TEntity entity);

        public Task<TEntity?> DeleteByIdAsync(int id);
    }
}
