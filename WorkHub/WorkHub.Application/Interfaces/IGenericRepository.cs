using System.ComponentModel;

namespace WorkHub.Application.Interfaces
{
    public interface IGenericRepository<T,Tkey> where T : class 
    {
        public Task<T?> GetByIdAsync(Tkey id);
        public Task<IEnumerable<T>> GetAllAsync(int page=1, int pageSize=10);

        public Task Add(T entity);
        public Task Update(T entity);
        public Task Delete(Tkey id);
    }
}
