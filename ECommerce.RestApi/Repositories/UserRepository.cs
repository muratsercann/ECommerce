using ECommerce.RestApi.Models;
using MongoDB.Driver;

namespace ECommerce.RestApi.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly ECommerceContext _mongoContext;

        public UserRepository(ECommerceContext mongoContext) : base(mongoContext)
        {
            _mongoContext = mongoContext;
        }

        public async Task<IEnumerable<string>> GetFavoriteIdsAsync(string userId)
        {
            FilterDefinition<User> filter = Builders<User>.Filter.Eq(user => user.Id, userId);

            List<string> favoriteIds = await _mongoContext.Users
              .Find(u => u.Id == userId)
              .Project(u =>
                  u.Favorites
              ).FirstOrDefaultAsync();

            return favoriteIds;

        }
    }
}
