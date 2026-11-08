using System.ComponentModel;
using System.Linq.Expressions;

namespace WorkHub.Application.Interfaces
{
    public interface IGenericRepository<T,Tkey> where T : class 
    {
        public Task<T?> GetByIdAsync(Tkey id, string includeProperties = "");
        public Task<IEnumerable<T>> GetAllAsync(int page=1, int pageSize=10, Expression<Func<T, bool>>? filter = null);

        public Task Add(T entity);
        public Task Update(T entity);
        public Task Delete(Tkey id);

        public Task<int> CountAsync(Expression<Func<T,bool>>? filter=null); 
    }
}
