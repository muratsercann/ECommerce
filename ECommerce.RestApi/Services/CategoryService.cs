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

        public async Task<Category> DeleteAsync(Category category)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _mongoContext.Categories.AsQueryable().ToListAsync();
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
