using ECommerce.RestApi.Models;
using ECommerce.RestApi.Models.DTOs;
using MongoDB.Bson;

namespace ECommerce.RestApi.Services
{

    public interface IUserService
    {
        Task<User> GetUserAsync(string userId);
        Task<bool> IsExistingUser(string username);

        Task<List<User>> GetAllUserAsync();

        Task<long> GetUsersCountAsync();

        Task<User> CreateOneAsync(User user);

        Task<List<User>> CreateManyAsync(List<User> users);

        Task<User> UpdateAsync(User user);

        Task<bool> DeleteAsync(User user);

        Task<Cart> UpdateCartAsync(string userId,Cart cart);

        Task<List<string>> UpdateFavoritesAsync(string userId, List<string>productId);

        Task<IEnumerable<Product>> GetFavoriteProductsAsync(string userId);

        Task<IEnumerable<Product>> GetProductsInTheCartAsync(string userId);
    }
}
