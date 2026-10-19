using Microsoft.EntityFrameworkCore;
using WorkHub.Application.Interfaces;
using WorkHub.Infrastructure.Data;

namespace WorkHub.Infrastructure.Repositories
{
    public class GenericRepository<T, TKey>  :IGenericRepository<T, TKey> where T :class where TKey : notnull
    {
        private readonly AppDbContext _dbContext;

        public GenericRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            return Task.CompletedTask;
        }

        public async Task Delete(TKey id)
        {
            T? entity = await _dbContext.Set<T>().FindAsync(id);
            if(entity != null)
            {
                _dbContext.Set<T>().Remove(entity);
            }

          return ;
        }

        public async Task<IEnumerable<T>> GetAllAsync(int page = 1, int pageSize = 10)
        {
            if(page <= 0 || pageSize <= 0)
            {
                throw new ArgumentException("Page and pageSize must be greater than zero.");
            }
            return await _dbContext.Set<T>().Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<T?> GetByIdAsync(TKey id)
        {
          T? result = await _dbContext.Set<T>().FindAsync(id);
            return result;
        }

        public Task Update(T entity)
        {
           _dbContext.Set<T>().Update(entity);
            return Task.CompletedTask;
        }
    }
}
