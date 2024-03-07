using ECommerce.RestApi.Dto;
using ECommerce.RestApi.Models;
using System.Linq.Expressions;

namespace ECommerce.RestApi.Repositories
{
    public interface IFavoriteRepository
    {
        Task<IEnumerable<TResult>> GetByUserIdAsync<TResult>(string userId,
            Expression<Func<Product,TResult>> selector);

        Task<bool> UpdateFavoritesAsync(string userId, IEnumerable<string> favoriteIds);
    }
}
