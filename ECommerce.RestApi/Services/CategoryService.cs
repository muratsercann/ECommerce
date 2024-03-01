using ECommerce.RestApi.Models;
using MongoDB.Driver;

namespace ECommerce.RestApi.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ECommerceContext _mongoContext;

        public CategoryService(ECommerceContext mongoContext)
        {
            _mongoContext = mongoContext;
        }
        public async Task<List<Category>> CreateManyAsync(List<Category> categories)
        {
            await _mongoContext.Categories.InsertManyAsync(categories);
            return categories;
        }

        public async Task<Category> CreateOneAsync(Category category)
        {
            await _mongoContext.Categories.InsertOneAsync(category);
            return category;
        }

        public async Task<bool> DeleteAsync(string categoryId)
        {
            var filter = Builders<Category>.Filter.Eq(c => c.Id, categoryId);
            var result = await _mongoContext.Categories.DeleteOneAsync(filter);
            return result.IsAcknowledged;
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _mongoContext.Categories.AsQueryable().ToListAsync();
        }

        public async Task<Category> GetCategoryAsync(string categoryId)
        {
            var filter = Builders<Category>.Filter.Eq(c => c.Id, categoryId);
            return await _mongoContext.Categories.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<long> GetCategoryCountAsync()
        {
            return await _mongoContext.Categories.CountDocumentsAsync(FilterDefinition<Category>.Empty);
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            throw new NotImplementedException();
        }
    }
}
