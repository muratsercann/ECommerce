using ECommerce.RestApi.Models;

namespace ECommerce.RestApi.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<string>> GetFavoriteIdsAsync(string userId);
    }
}
