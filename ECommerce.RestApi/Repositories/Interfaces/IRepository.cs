using System.Linq.Expressions;

namespace ECommerce.RestApi.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(string id);

        Task<TResult> GetByIdAsync<TResult>(string id, Expression<Func<TEntity, TResult>> selector);
        
        Task<IEnumerable<TResult>> GetByIdAsync<TResult>(IEnumerable<string> ids, Expression<Func<TEntity, TResult>> selector);
       
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<IEnumerable<TResult>> GetAllAsync<TResult>(Expression<Func<TEntity, TResult>> selector);

        Task<bool> ExistsAsync(string id);

        Task<long> GetCountAsync();

        Task<bool> AddAsync(TEntity entity);

        Task<bool> AddAsync(IEnumerable<TEntity> entities);

        Task<bool> DeleteAsync(string id);

    }
}
