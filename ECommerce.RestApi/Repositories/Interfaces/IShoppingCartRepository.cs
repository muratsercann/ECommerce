using ECommerce.RestApi.Dto;
using ECommerce.RestApi.Models;
using System.Linq.Expressions;

namespace ECommerce.RestApi.Repositories
{
    public interface IShoppingCartRepository
    {
        Task<ShoppingCart> GetShoppingCartAsync(string userId); 

        Task<IEnumerable<TResult>> GetProductsAsync<TResult>(IEnumerable<string> productIds, Expression<Func<Product, TResult>> selector);

        Task<bool> UpdateShoppingCartAsync(string userId, ShoppingCart shopping);
    }
}
