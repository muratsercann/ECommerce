using ECommerce.RestApi.Dto;
using ECommerce.RestApi.Models;
using System.Linq.Expressions;

namespace ECommerce.RestApi.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<TResult>> GetByCategoryAsync<TResult>(string categoryId, Expression<Func<Product, TResult>> selector);
    }
}
