using ECommerce.RestApi.Models;
using MongoDB.Bson.Serialization.IdGenerators;
using System.Linq.Expressions;

namespace ECommerce.RestApi.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<string>> GetFavoriteIdsAsync(string userId);
    }
}
