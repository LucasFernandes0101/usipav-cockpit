using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using usipav_cockpit.Domain.Base;
using usipav_cockpit.Domain.Interfaces;
using usipav_cockpit.Infrastructure.Contexts;

namespace usipav_cockpit.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, IBaseEntity
    {
        protected SqlDbContext _dbContext { get; set; }

        public BaseRepository(SqlDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);

            entity.Active = false;

            await UpdateAsync(entity);
        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>>? criteria = null)
        {
            if (criteria is null)
                return await _dbContext.Set<T>().ToListAsync();
            else
                return await _dbContext.Set<T>().Where(criteria).ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbContext.FindAsync<T>(id);
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
