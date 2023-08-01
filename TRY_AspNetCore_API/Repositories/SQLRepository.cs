using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query;
using System.Globalization;
using System.Linq.Expressions;
using TRY_AspNetCore_API.Data;
using TRY_AspNetCore_API.Models.Domain;

namespace TRY_AspNetCore_API.Repositories
{
    public class SQLRepository<TDbContext, TEntity> : IRepository<TEntity>
        where TDbContext : DbContext
        where TEntity : class, IEntityHasId
    {
        private readonly TDbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public SQLRepository(TDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }


        private IQueryable<TEntity> IncludeMultiple(
            IQueryable<TEntity> query,
            Expression<Func<TEntity, object>>[]? includeExpressions)
        {
            if (includeExpressions == null)
            {
                return query;
            }

            // Chain each Include expression to query
            return includeExpressions.Aggregate(query,
                (currentQuery, includeExpression) => currentQuery.Include(includeExpression));
        }


        private IQueryable<TEntity> WhereMultiple(
            IQueryable<TEntity> query,
            Expression<Func<TEntity, bool>>[]? filterExpressions)
        {
            if (filterExpressions == null)
            {
                return query;
            }

            // Chain each Where expression to query 
            return filterExpressions.Aggregate(query,
                  (currentQuery, filterExpression) => currentQuery.Where(filterExpression));
        }


        private IQueryable<TEntity> OrderByMultiple(
            IQueryable<TEntity> query,
            Expression<Func<TEntity, object>>[]? orderByExpressions = null,
            bool[]? orderByIsAscending = null)
        {
            if (orderByExpressions == null)
            {
                return query;
            }

            var resultQuery = query;

            // Loop through each orderByExpression and chain it to query
            for (var i = 0; i < orderByExpressions.Length; i++)
            {
                var orderByExpression = orderByExpressions[i];
                // Defaults to ascending
                var isAscending = true;

                // Check if orderByIsAscending is provided for each orderByExpression
                // Else default to ascending
                if (orderByIsAscending != null && i < orderByIsAscending.Length)
                {
                    isAscending = orderByIsAscending[i];
                }

                // Chain query
                resultQuery = isAscending ?
                    resultQuery.OrderBy(orderByExpression) :
                    resultQuery.OrderByDescending(orderByExpression);
            }

            return resultQuery;
        }


        public async Task<WithCount<List<TEntity>>> GetAllAsync(
            Expression<Func<TEntity, bool>>[]? filters = null,
            Expression<Func<TEntity, object>>[]? includes = null,
            Expression<Func<TEntity, object>>[]? sortBy = null,
            bool[]? sortByIsAscending = null,
            int pageNumber = 1,
            int pageSize = 20)
        {
            IQueryable<TEntity> query = _dbSet.AsQueryable();

            // Include other entities
            query = IncludeMultiple(query, includes);

            // Filter
            query = WhereMultiple(query, filters);

            // Sort
            query = OrderByMultiple(query, sortBy, sortByIsAscending);

            // Keep order consistent if sortBy result has ties due to sorting by low-cardinality attributes
            query = query.OrderBy(x => x.Id);

            // Pagination
            var skipResults = (pageNumber - 1) * pageSize;
            query = query.Skip(skipResults).Take(pageSize);

            // Get list
            var list = await query.ToListAsync();

            // Count
            IQueryable<TEntity> countQuery = _dbSet.AsQueryable();
            countQuery = WhereMultiple(countQuery, filters);

            var count = await countQuery.CountAsync();

            return new WithCount<List<TEntity>> { Data = list, Count = count };
        }


        public async Task<TEntity?> GetOneByIdAsync(
            int id,
            Expression<Func<TEntity, object>>[]? includes = null)
        {
            IQueryable<TEntity> query = _dbSet.AsQueryable();

            // Include other entities
            query = IncludeMultiple(query, includes);

            // Get only one result with matching Id
            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }


        public async Task<TEntity?> GetOneByFilterAsync(
            Expression<Func<TEntity, bool>>[] filters,
            Expression<Func<TEntity, object>>[]? includes = null)
        {

            IQueryable<TEntity> query = _dbSet.AsQueryable();

            // Include other entities
            query = IncludeMultiple(query, includes);

            // Filter
            query = WhereMultiple(query, filters);

            // Get only first result
            return await query.FirstOrDefaultAsync();
        }


        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }


        public async Task<TEntity?> UpdateByIdAsync(int id, TEntity entity)
        {
            if (id != entity.Id)
            {
                return null;
            }

            var existingEntity = await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (existingEntity == null)
            {
                return null;
            }

            if (entity is IEntityHasTimeStamp)
            {
                // TODO: double check if updatedDate successfully updated
                ((IEntityHasTimeStamp)entity).UpdatedDate = DateTime.UtcNow;
            }

            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();

            // TODO: double check if this returns updated entity or previous entity because no tracking
            return existingEntity;
        }


        public async Task<TEntity?> DeleteByIdAsync(int id)
        {
            var existingEntity = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);

            if (existingEntity == null)
            {
                return null;
            }

            _dbSet.Remove(existingEntity);
            await _dbContext.SaveChangesAsync();

            return existingEntity;
        }
    }
}
